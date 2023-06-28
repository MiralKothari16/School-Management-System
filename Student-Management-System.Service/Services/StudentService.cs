using Student_Management_System.Service.DTO;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.Interface;
using AutoMapper;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studnet_Management_System.Model.Interface;
using Studnet_Management_System.Model;
using Student_Management_System.Service.DTO.GetDTO;
using Studnet_Management_System.Model.Repository;

namespace Student_Management_System.Service.Services
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailsService _mailsService;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAttendenceRepository _attendenceRepository;
        private readonly IGradeBookRepository _gradeBookRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public StudentService(IUserRepository userRepository, IMapper mapper, IStudentRepository studentRepository, IMailsService mailsService, ITeacherRepository teacherRepository, IAttendenceRepository attendenceRepository, IGradeBookRepository gradeBookRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _attendenceRepository = attendenceRepository;
            _gradeBookRepository = gradeBookRepository;
        }
        #endregion
        public ResponseDTO AddStudent(AddStudentDTO student)
        {
            var response = new ResponseDTO();
            try
            {
                if (student.Name != "")
                {
                    var resultEmail = _studentRepository.GetStudentByEmail(student.Email);
                    // if (resultEmail != null)
                    //{
                    //    response.Status = 400;
                    //    response.Message = "Bad Request";
                    //    response.Error = "Email already exist.";
                    //    return response;
                    //}
                    var lastrollno = _studentRepository.GetStudentrollnoClasswise(student.Class, student.DateOfAdmission);
                    if (lastrollno != null)
                    {
                        student.RollNo = (Convert.ToInt32(lastrollno.RollNo) + 1).ToString();
                    }
                    else
                    {
                        student.RollNo = "1";
                    }
                    student.IsActive = true;
                    //var addstudent = _studentRepository.AddStudent(_mapper.Map<Student>(student));
                    //if (addstudent > 0)
                    //{
                    //    var resultbyId = _studentRepository.GetStudentById(addstudent);
                    //    if (resultbyId != null)
                    //    {
                    //        var role = new User
                    //        {
                    //            Email = student.Email,
                    //            Password = student.Password,
                    //            RoleId = 3,
                    //            IsActive = true,
                    //            RegisterrdId = resultbyId.Id,

                    //        };
                    //        _userRepository.AddUser(role);
                    //    }
                    //}
                    //send Email to candidate
                    var mailtostudent = new MailDTO
                    {
                        To = student.Email,
                        Subject = "Welcome ! ",//+ user.FirstName + " " + user.LastName ,
                        Body = $"<p>Welcome, {student.Name} is enrolled on date : {student.DateOfAdmission} for class {student.Class}.</p>"
                    };
                    _mailsService.SendMail(mailtostudent);


                    var teacheremail = _teacherRepository.GetClassTeacher(student.Class);
                    if (teacheremail != null)
                    {
                        var mailtoteacher = new MailDTO
                        {
                            To = teacheremail,
                            Subject = "Allocation ! ",//+ user.FirstName + " " + user.LastName ,
                            Body = $"<p>Hello ,Student :  {student.Name} enrolled on date : {student.DateOfAdmission} is assigned for class {student.Class}.</p>"

                        };
                        _mailsService.SendMail(mailtoteacher);
                    }

                    response.Status = 204;
                    response.Message = "Student Created";
                }
                else
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Name not found.";
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

        public ResponseDTO DeleteStudent(int Id)
        {
            var response = new ResponseDTO();
            try
            {
                var studentById = _studentRepository.GetStudentById(Id);
                if (studentById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "student not found.";
                    return response;
                }
                studentById.IsActive = false;
                var deleteFlag = _studentRepository.DeleteStudent(studentById);
                if (deleteFlag)
                {
                    response.Status = 200;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Student Not Deleted";
                    response.Error = "Student Not Deleted";
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
        //student can see
        public ResponseDTO GetStudentAttendencesubjwise(int Id, int cyear, string subject)
        {
            var response = new ResponseDTO();
            try
            {
                var resultStudentId = _studentRepository.GetStudentById(Id);
                if (resultStudentId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student not Found.";
                    return response;
                }
                var resultyearatt = _attendenceRepository.CheckattYearwise(Id, cyear);
                if (resultyearatt == false)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student attendence for this year not found.";
                    return response;
                }
                var resultsubatt = _attendenceRepository.Checkattsubjectwise(Id, subject);
                if (resultsubatt == false)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student attendence for this subject  not found.";
                    return response;
                }
                //var attendence = _mapper.Map<List<GetAttendenceDTO>>(_attendenceRepository.GetAttByTeacherorStudentId(Id, cyear).ToList());
                var attendence = _attendenceRepository.GetStudentAttendencesubjwise(Id, cyear, subject);
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
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetStudentByEmail(string Email)
        {
            var response = new ResponseDTO();
            try
            {
                var resultemail = _studentRepository.GetStudentByEmail(Email);
                if (resultemail == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Student not found.";
                    return response;
                }
                else
                {
                    var result = _mapper.Map<GetStudentDTO>(resultemail);
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

        public ResponseDTO GetStudentById(int Id)
        {
            var response = new ResponseDTO();
            try
            {
                var resultStudentId = _studentRepository.GetStudentById(Id);
                if (resultStudentId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student not Found.";
                    return response;
                }
                else
                {
                    var result = _mapper.Map<GetStudentDTO>(resultStudentId);
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
        //student can see
        public ResponseDTO GetStudentMarkssubjwise(int Id, string subject, DateTime examdate)
        {
            var response = new ResponseDTO();
            try
            {
                var resultStudentId = _studentRepository.GetStudentById(Id);
                if (resultStudentId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student not Found.";
                    return response;
                }
                var resultyearatt = _gradeBookRepository.Checkmarksexamwise(Id, examdate);
                if (resultyearatt == false)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student marks for this examdate not found.";
                    return response;
                }
                var resultsubatt = _gradeBookRepository.Checkmarkssubjectwise(Id, subject);
                if (resultsubatt == false)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Student marks for this subject  not found.";
                    return response;
                }

                var marksResult = _gradeBookRepository.GetStudentMakssubjwise(Id, subject, examdate);
                if (marksResult != null)
                {
                    response.Status = 200;
                    response.Data = marksResult;
                    response.Message = "Ok";
                    return response;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Found";
                    response.Error = "Studnet marks of this subject for this examdate is not found";
                    return response;
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
        //for admin
        public ResponseDTO GetStudents()
        {
            var response = new ResponseDTO();
            try
            {
                var users = _mapper.Map<List<GetStudentDTO>>(_studentRepository.GetStudents().ToList());
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

        public ResponseDTO UpdateStudent(UpdateStudentDTO student)
        {
            var response = new ResponseDTO();
            try
            {
                var resultId = _studentRepository.GetStudentById(student.Id);
                if (resultId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Student not found.";
                    return response;
                }
                var resultEmail = _studentRepository.GetStudentByEmail(student.Email);
                if (resultEmail != null && resultEmail.Id != student.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Created.";
                    response.Error = "Student with this email already exist.";
                    return response;
                }
                var updateStudent = _studentRepository.UpdateStudent(_mapper.Map<Student>(student));

                if (updateStudent != null)
                {
                    response.Status = 204;
                    response.Message = "Student Updated Successfully.";
                    return response;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated.";
                    response.Error = "Student is not updated.";
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
    }
}
