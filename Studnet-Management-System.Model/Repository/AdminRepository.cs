using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Repository
{
    public  class AdminRepository :IAdminRepository
    {
        #region Field
        private readonly StudMgtContext _context;
        #endregion
        #region Constructor
        public AdminRepository(StudMgtContext context)
        {
            _context = context;
        }

        public int AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            if (_context.SaveChanges() > 0) { return admin.Id; } else { return 0; }
        }

        public bool DeleteAdmin(Admin admin)
        {
            _context.Entry(admin).Property("IsActive").IsModified = false;
            return _context.SaveChanges() > 0;
        }

        public Admin GetAdminByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(x => x.Email == email);
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.FirstOrDefault(x => x.Id == id);
        }
        #endregion
        #region Methods
        public IEnumerable<Admin> GetAdmins()
        {
            return _context.Admins.Where(x => x.IsActive == true).ToList();
        }

        public bool UpdateAdmin(Admin admin)
        {
            _context.Admins.Update(admin);
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
