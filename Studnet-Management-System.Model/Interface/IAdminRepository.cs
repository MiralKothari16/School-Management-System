using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAdmins();

        int AddAdmin(Admin admin);
        bool UpdateAdmin(Admin admin);
        bool DeleteAdmin(Admin admin);

        Admin GetAdminByEmail(string email);

        Admin GetAdminById(int id);
    }
}
