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

namespace Student_Management_System.Service.Services
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public StudentService(IUserRepository userRepository, IMapper mapper, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        #endregion
        public ResponseDTO AddStudent(AddStudentDTO student)
        {
            var response = new ResponseDTO();
            try
            {
                var resultEmail = _studentRepository.GetStudentByEmail(student.Email);
                if (resultEmail != null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Email already exist.";
                    return response;
                }
                student.IsActive = true;
                var addstudent = _studentRepository.AddStudent(_mapper.Map<Student>(student));
                if (addstudent > 0)
                {
                    var resultbyId = _studentRepository.GetStudentById(addstudent);
                    if (resultbyId!=null)
                    {
                        var role = new User
                        {
                            Email = student.Email,
                            Password = student.Password,
                            RoleId = 3,
                            IsActive = true,
                            RegisterrdId = resultbyId.Id,

                        };
                        _userRepository.AddUser(role);
                    }
                }
                response.Status = 204;
                response.Message = "Student Created";
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
                    response.Error = "User not found.";
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
                    response.Error = "User not found.";
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
                    response.Error = "User not Found.";
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
