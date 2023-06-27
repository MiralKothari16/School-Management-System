using Microsoft.Extensions.Logging.Abstractions;
using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        #region Field
        private readonly StudMgtContext _context;
        #endregion
        #region Constructor
        public TeacherRepository(StudMgtContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public int AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            if (_context.SaveChanges() > 0) { return teacher.Id; } else { return 0; }
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            _context.Entry(teacher).Property("IsActive").IsModified = false;
            return _context.SaveChanges() > 0;
        }

        public Teacher GetTeacherByEmail(string email)
        {
            return _context.Teachers.FirstOrDefault(x => x.Email == email);
        }

        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return _context.Teachers.Where(x => x.IsActive == true).ToList();
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            return _context.SaveChanges() > 0;
        }
        public string GetSubject(string subj,int id)
        {
            var sub = _context.Teachers.FirstOrDefault(x => x.Id == id && x.Subject == subj );
            if (sub != null) { return sub.Subject; } else { return null; }
        }

        public string GetClass(string clas, int id)
        {
            var cls = _context.Teachers.FirstOrDefault(x => x.Id == id && x.Class == clas);
            if (cls != null) { return cls.Class; } else { return null; }
        }
        public string GetClassTeacher(string cls)
        {
            var teacheremail = _context.Teachers.FirstOrDefault(x => x.Class == cls && x.IsActive == true);
            if(teacheremail!=null) {  return teacheremail.Email; } else { return null; }
                }

        #endregion
    }
}
