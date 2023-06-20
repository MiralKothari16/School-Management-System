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

        #endregion
    }
}
