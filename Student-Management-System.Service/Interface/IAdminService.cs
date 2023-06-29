
using Org.BouncyCastle.Ocsp;
using Student_Management_System.Service.DTO;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Interface
{
    public interface IAdminService
    {
        ResponseDTO GetAdmins();
        ResponseDTO AddAdmin(AddAdminDTO admin);
        ResponseDTO UpdateAdmin(UpdateAdminDTO admin);
        ResponseDTO DeleteAdmin(int id);
        ResponseDTO GetAdminById( int id);
        ResponseDTO GetAdminByEmail(string email);

        ResponseDTO GetStudentsAttendence();
        ResponseDTO GetStudentMarks();

    }
}
