using Student_Management_System.Service.DTO;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.Interface;
using Studnet_Management_System.Model.Interface;
using Studnet_Management_System.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Student_Management_System.Service.Services
{
    public class UserService : IUserService
    {
        #region fields
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Constr
        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion
        #region Methods
        // public List<string> GetUserByEmail(string email)

        public ResponseDTO GetUsers()
        {
            var response = new ResponseDTO();
            try
            {
                var users = _mapper.Map<List<GetUserDTO>>(_userRepository.GetUsers().ToList());
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
        public ResponseDTO GetUserByEmail(string email)
        {
            var response = new ResponseDTO();
            try
            {
                var resultemail = _userRepository.GetUserByEmail(email);
                if (resultemail == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found.";
                    response.Error = "User not found.";
                    return response;
                }
                else
                {
                    var result = _mapper.Map<GetUserDTO>(resultemail);
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

        public GetUserDTO IsUserExists(TokenDTO model)
        {
            //var user = _userRepository.GetUserByEmail(model.Email);
            var user = _userRepository.GetUsers().FirstOrDefault(x => x.Email.ToLower() == model.Username.ToLower());// && x.Password == model.Password);
            if (user == null || user.Password !=model.Password)// _hasherService.Hash(model.Password))
            {
                return null;
                //throw new Exception("User not found");
            }
            else { }
            return _mapper.Map<GetUserDTO>(user);
        }

        public List<string> GetRoles(string email)
        {
            return _userRepository.GetRoles(email);
        }


        #endregion
    }
}
