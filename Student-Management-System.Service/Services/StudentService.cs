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
                    //if (student.IsAdmin)
                    //{
                    var role = new User
                    {
                        Email = student.Email,
                        Password = student.Password,
                        RoleId = 3
                    };
                    _userRepository.AddUser(role);
                    // }
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
            throw new NotImplementedException();
        }

        public ResponseDTO GetStudentByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetStudentById(int Id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
