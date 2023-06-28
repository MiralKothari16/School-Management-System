using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management_System.Service.DTO.GetDTO;

namespace Student_Management_System.Service.Interface
{
    public interface IUserService
    {

        ResponseDTO GetUsers();
        ResponseDTO GetUserByEmail(string Email);

        GetUserDTO IsUserExists(TokenDTO model);

        List<string> GetRoles(string email);
    }
}
