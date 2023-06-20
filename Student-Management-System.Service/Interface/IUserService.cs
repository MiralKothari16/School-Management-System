using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Interface
{
    public interface IUserService
    {
        
        // ResponseDTO UserPagination(int page, int content);
        ResponseDTO AddUser(AddUserDTO user);
        ResponseDTO UpdateUser(UpdateUserDTO user);
        ResponseDTO DeleteUser(int Id);
        ResponseDTO GetUserByEmail(string Email);
    }
}
