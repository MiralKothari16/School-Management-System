using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly StudMgtContext _context;
        #endregion
        #region Constructor
        public UserRepository(StudMgtContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods

        public IEnumerable<User> GetUsers()
        {
            // return _context.Users(x => x.IsActive == true ));
            return _context.Users.Where(x => x.IsActive == true).ToList();
        }
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            if (_context.SaveChanges() > 0) { return user.Id; } else { return 0; }            
        }

        public bool Updateuser(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }

        public bool Deleteuser(User user)
        {
          _context.Entry(user).Property("IsActive").IsModified=true;
            return _context.SaveChanges() > 0;  
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }
        #endregion
    }
}
