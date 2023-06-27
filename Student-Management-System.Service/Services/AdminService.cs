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
    public class AdminService :IAdminService
    {
        #region Fields
        private readonly IAdminRepository _amdinRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttendenceRepository _attendenceRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AdminService(IUserRepository userRepository, IMapper mapper, IAdminRepository adminRepository,IAttendenceRepository attendenceRepository)
        {
            _amdinRepository = adminRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _attendenceRepository = attendenceRepository;
        }

        public ResponseDTO AddAdmin(AddAdminDTO admin)
        {
            var response = new ResponseDTO();
            try
            {
                var resultEmail = _amdinRepository.GetAdminByEmail(admin.Email);
                if (resultEmail != null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Email already exist.";
                    return response;
                }
                admin.IsActive = true;
                admin.CreatedDate = DateTime.Now; ;
                var addadmin = _amdinRepository.AddAdmin(_mapper.Map<Admin>(admin));
                if (addadmin > 0)
                {
                    var resultbyId = _amdinRepository.GetAdminById(addadmin);
                    if (resultbyId != null)
                    {
                        var role = new User
                        {
                            Email = admin.Email,
                            Password = admin.Password,
                            RoleId = 1,
                            IsActive = true,
                            RegisterrdId = resultbyId.Id,
                        };
                        _userRepository.AddUser(role);
                    }
                }
                response.Status = 204;
                response.Message = "Admin Created";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO DeleteAdmin(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var adminById = _amdinRepository.GetAdminById(id);
                if (adminById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "admin not found.";
                    return response;
                }
                adminById.IsActive = false;
                var deleteFlag = _amdinRepository.DeleteAdmin(adminById);
                if (deleteFlag)
                {
                    response.Status = 200;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Admin Not Deleted";
                    response.Error = "Admin Not Deleted";
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

        public ResponseDTO GetAdminByEmail(string email)
        {
            var response = new ResponseDTO();
            try
            {
                var resultemail = _amdinRepository.GetAdminByEmail(email);
                if (resultemail == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Admin not found.";
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

        public ResponseDTO GetAdminById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var resultAdminId = _amdinRepository.GetAdminById(id);
                if (resultAdminId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Admin not Found.";
                    return response;
                }
                else
                {
                    var result = _mapper.Map<GetAdminDTO>(resultAdminId);
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

        public ResponseDTO GetAdmins()
        {
            var response = new ResponseDTO();
            try
            {
                var users = _mapper.Map<List<GetAdminDTO>>(_amdinRepository.GetAdmins().ToList());
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

        public ResponseDTO UpdateAdmin(UpdateAdminDTO admin)
        {
            var response = new ResponseDTO();
            try
            {
                var resultId = _amdinRepository.GetAdminById(admin.Id);
                if (resultId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "Admin not found.";
                    return response;
                }
                var resultEmail = _amdinRepository.GetAdminByEmail(admin.Email);
                if (resultEmail != null && resultEmail.Id != admin.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Created.";
                    response.Error = "Admin with this email already exist.";
                    return response;
                }
                var updateAdmin = _amdinRepository.UpdateAdmin(_mapper.Map<Admin>(admin));

                if (updateAdmin != null)
                {
                    response.Status = 204;
                    response.Message = "Admin Updated Successfully.";
                    return response;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated.";
                    response.Error = "Admin is not updated.";
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

        public ResponseDTO GetStudentsAttendence()
        {
            var response = new ResponseDTO();
            try
            {
                var users =_attendenceRepository.GetStudentsAttendence();
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
        #endregion

        

    }
}
