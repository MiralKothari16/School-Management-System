using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Services
{
    public class TeacherService :ITeacherService
    {
        #region Fields
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TeacherService(IUserRepository userRepository, IMapper mapper, ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
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

        #endregion
    }
}
