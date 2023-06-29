using AutoMapper;
using Microsoft.VisualBasic;
using Student_Management_System.Service.DTO;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.Interface;
using Studnet_Management_System.Model;
using Studnet_Management_System.Model.Interface;
using Studnet_Management_System.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Services
{
    public class TeacherService : ITeacherService
    {
        #region Fields
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttendenceRepository _attendenceRepository;
        private readonly IGradeBookRepository _gradeBookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TeacherService(IUserRepository userRepository, IMapper mapper, ITeacherRepository teacherRepository, IAttendenceRepository attendenceRepository, IGradeBookRepository gradeBookRepository, IStudentRepository studentRepository)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _attendenceRepository = attendenceRepository;
            _gradeBookRepository = gradeBookRepository;
            _studentRepository = studentRepository;
        }

        public ResponseDTO AddTeacher(AddTeacherDTO teacher)
        {
            var response = new ResponseDTO();
            try
            {
                var resultEmail = _teacherRepository.GetTeacherByEmail(teacher.Email);
                if (resultEmail != null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Email already exist.";
                    return response;
                }

                var teachers = _teacherRepository.GetTeachers().ToList();
                if (teachers.Count <= 12)
                {
                    teacher.IsActive = true;
                    var addteacher = _teacherRepository.AddTeacher(_mapper.Map<Teacher>(teacher));
                    if (addteacher > 0)
                    {
                        var resultbyId = _teacherRepository.GetTeacherById(addteacher);
                        if (resultbyId != null)
                        {
                            var role = new User
                            {
                                Email = teacher.Email,
                                Password = teacher.Password,
                                RoleId = 2,
                                IsActive = true,
                                RegisterrdId = resultbyId.Id,

                            };
                            _userRepository.AddUser(role);
                        }
                    }
                    response.Status = 204;
                    response.Message = "Teacher Created";
                }
                else
                {
                    response.Status = 201;
                    // response.Message = "Bad Request";
                    response.Error = "Can insert only 12 teachers";
                    return response;

                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO AddAttendence(AddAttendenceDTO attendence)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _teacherRepository.GetTeacherById(attendence.teacherId);
                if (result == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not found";
                    return response;
                }
                var subj = _teacherRepository.GetSubject(attendence.Subject, attendence.teacherId);
                if (subj == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Subject is not studied by the teacher";
                    return response;
                }
                var Cls = _teacherRepository.GetClass(attendence.Class, attendence.teacherId);
                if (Cls == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Class is not allocated to this teacher";
                    return response;
                }
                var presence = _attendenceRepository.IsPresentToday(attendence.teacherId, attendence.studentId, attendence.currentdate);
                if (presence == true)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Today's attendence is taken.";
                    return response;
                }
                _attendenceRepository.AddAttendence(_mapper.Map<Attendence>(attendence));
                response.Status = 201;
                response.Message = "Attendence is added";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error.";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO UpdateAttendence(UpdateAttendenceDTO attendence)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _teacherRepository.GetTeacherById(attendence.teacherId);
                if (result == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not found";
                    return response;
                }
                var subj = _teacherRepository.GetSubject(attendence.subject, attendence.teacherId);
                if (subj == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Subject is not studied by the teacher";
                    return response;
                }
                var Cls = _teacherRepository.GetClass(attendence.Class, attendence.teacherId);
                if (Cls == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Class is not allocated to this teacher";
                    return response;
                }
                var presence = _attendenceRepository.IsPresentToday(attendence.teacherId, attendence.studentId, attendence.currentdate);
                if (presence != true)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Today's attendence is not taken.";
                    return response;
                }
                // attendence.Subject = result.Subject;
                //attendence.Class = result.Class;
                //attendence.Presence = attendence.Presence;
                //attendence.currentdate = DateTime.UtcNow;

                _attendenceRepository.UpdateAttendene(_mapper.Map<Attendence>(attendence));
                response.Status = 201;
                response.Message = "Attendence is Updated";
                // response.Data = Convert.ToInt32(addans);

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error.";
                response.Error = ex.Message;
            }
            return response;

        }

        public ResponseDTO DeleteTeacher(int Id)
        {
            var response = new ResponseDTO();
            try
            {
                var teacherById = _teacherRepository.GetTeacherById(Id);
                if (teacherById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "User not found.";
                    return response;
                }
                teacherById.IsActive = false;
                var deleteFlag = _teacherRepository.DeleteTeacher(teacherById);
                if (deleteFlag)
                {
                    response.Status = 200;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Teacher Not Deleted";
                    response.Error = "Teacher Not Deleted";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetTeacherByEmail(string Email)
        {
            var response = new ResponseDTO();
            try
            {
                var resultemail = _teacherRepository.GetTeacherByEmail(Email);
                if (resultemail == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Teacher not found.";
                    return response;
                }
                else
                {
                    var result = _mapper.Map<GetTeacherDTO>(resultemail);
                    response.Status = 200;
                    response.Data = result;
                    response.Message = "Ok";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error.";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetTeacherById(int Id)
        {
            var response = new ResponseDTO();
            try
            {
                var resultTeacherId = _teacherRepository.GetTeacherById(Id);
                if (resultTeacherId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not Found.";
                    return response;
                }
                else
                {
                    var result = _mapper.Map<GetTeacherDTO>(resultTeacherId);
                    response.Status = 200;
                    response.Data = result; ;
                    response.Message = "Ok";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }
            return response;
        }

       


        public ResponseDTO GetTeachers()
        {
            var response = new ResponseDTO();
            try
            {
                var users = _mapper.Map<List<GetTeacherDTO>>(_teacherRepository.GetTeachers().ToList());
                response.Status = 200;
                response.Data = users;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error.";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO UpdateTeacher(UpdateTeacherDTO teacher)
        {
            var response = new ResponseDTO();
            try
            {
                var resultId = _teacherRepository.GetTeacherById(teacher.Id);
                if (resultId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Teacher not found.";
                    return response;
                }
                var resultEmail = _teacherRepository.GetTeacherByEmail(teacher.Email);
                if (resultEmail != null && resultEmail.Id != teacher.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Created.";
                    response.Error = "Teacher with this email already exist.";
                    return response;
                }
                var updateTeacher = _teacherRepository.UpdateTeacher(_mapper.Map<Teacher>(teacher));

                if (updateTeacher != null)
                {
                    response.Status = 204;
                    response.Message = "Teacher Updated Successfully.";
                    return response;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated.";
                    response.Error = "Teacher is not updated.";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error.";
                response.Error = ex.Message;
            }
            return response;
        }
        //teacher get attendence
        public ResponseDTO GetAttByTeacherId(int Id, int cyear)
        {
            var response = new ResponseDTO();
            try
            {
                var resultTeacherId = _teacherRepository.GetTeacherById(Id);
                if (resultTeacherId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not Found.";
                    return response;
                }
                else
                {
                    //var attendence = _mapper.Map<List<GetAttendenceDTO>>(_attendenceRepository.GetAttByTeacherorStudentId(Id, cyear).ToList());
                    var attendence = _attendenceRepository.GetAttByTeacherId(Id, cyear);
                    if (attendence != null)
                    {
                        response.Status = 200;
                        response.Data = attendence;
                        response.Message = "Ok";
                        return response;
                    }
                    else
                    {
                        response.Status = 400;
                        response.Message = "Not found";
                        response.Error = "Attendence is not added";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO AddMarks(AddGradeBookDTO gradeBook)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _teacherRepository.GetTeacherById(gradeBook.teacherId);
                if (result == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not found";
                    return response;
                }
                var subj = _teacherRepository.GetSubject(gradeBook.Subject, gradeBook.teacherId);
                if (subj == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Subject is not studied by the teacher";
                    return response;
                }
                var Cls = _teacherRepository.GetClass(gradeBook.Class, gradeBook.teacherId);
                if (Cls == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Class is not allocated to this teacher";
                    return response;
                }
                var presence = _gradeBookRepository.IsMarksAdded(gradeBook.teacherId, gradeBook.studentId, gradeBook.ExamDate, gradeBook.Subject);
                if (presence == true)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Marks of this subject for this exam is added.";
                    return response;
                }
                _gradeBookRepository.AddMarks(_mapper.Map<GradeBook>(gradeBook));
                response.Status = 201;
                response.Message = "Marks is added";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error.";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO UpdateMarks(UpdateGradeBookDTO gradeBook)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _teacherRepository.GetTeacherById(gradeBook.teacherId);
                if (result == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not found";
                    return response;
                }
                var subj = _teacherRepository.GetSubject(gradeBook.Subject, gradeBook.teacherId);
                if (subj == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Subject is not studied by the teacher";
                    return response;
                }
                var Cls = _teacherRepository.GetClass(gradeBook.Class, gradeBook.teacherId);
                if (Cls == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "This Class is not allocated to this teacher";
                    return response;
                }
                var presence = _gradeBookRepository.IsMarksAdded(gradeBook.teacherId, gradeBook.studentId, gradeBook.ExamDate, gradeBook.Subject);
                if (presence != true)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Marks is not added of this subject for this exam";
                    return response;
                }
                // attendence.Subject = result.Subject;
                //attendence.Class = result.Class;
                //attendence.Presence = attendence.Presence;
                //attendence.currentdate = DateTime.UtcNow;

                _gradeBookRepository.UpdateMarks(_mapper.Map<GradeBook>(gradeBook));
                response.Status = 201;
                response.Message = "Marks is Updated";
                // response.Data = Convert.ToInt32(addans);

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error.";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetMarksTeacherIdwise(int Id, int cyear)
        {
            var response = new ResponseDTO();
            try
            {
                var resultTeacherId = _teacherRepository.GetTeacherById(Id);
                if (resultTeacherId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not Found.";
                    return response;
                }
                else
                {
                    //var attendence = _mapper.Map<List<GetAttendenceDTO>>(_attendenceRepository.GetAttByTeacherorStudentId(Id, cyear).ToList());
                    var resultmarks = _gradeBookRepository.GetMarksByTeacherId(Id, cyear);
                    if (resultmarks != null)
                    {
                        response.Status = 200;
                        response.Data = resultmarks; ;
                        response.Message = "Ok";
                        return response;
                    }
                    else
                    {
                        response.Status = 400;
                        response.Message = "Not found";
                        response.Error = "Marks is not added";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO getstudnetteacheridwise(int Id, int admissionyear)
        {
            var response = new ResponseDTO();
            try
            {
                var resultTeacherId = _teacherRepository.GetTeacherById(Id);
                if (resultTeacherId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Teacher not Found.";
                    return response;
                }
                else
                {
                    //var attendence = _mapper.Map<List<GetAttendenceDTO>>(_attendenceRepository.GetAttByTeacherorStudentId(Id, cyear).ToList());
                    var attendence = _studentRepository.GetStudentbyTeacherIdwise(Id, DateTime.Now.Year);
                    if (attendence != null)
                    {
                        response.Status = 200;
                        response.Data = attendence;
                        response.Message = "Ok";
                        return response;
                    }
                    else
                    {
                        response.Status = 400;
                        response.Message = "Not found";
                        response.Error = "Attendence is not added";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error.";
                response.Error = ex.Message;
            }
            return response;
        }

        #endregion
    }
}
