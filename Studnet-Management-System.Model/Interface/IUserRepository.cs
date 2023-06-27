using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        int AddUser(User user);

        bool Updateuser(User user);

        bool Deleteuser(User user);

        User GetUserByEmail(string email);
    }
}
