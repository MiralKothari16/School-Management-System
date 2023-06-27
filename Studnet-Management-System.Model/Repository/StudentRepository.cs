using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Repository
{
    public  class StudentRepository :IStudentRepository
    {
        #region Field
        private readonly StudMgtContext _context;
        #endregion
        #region Constructor
        public StudentRepository(StudMgtContext context)
        {
            _context = context; 
        }

        public int AddStudent(Student student)
        {
           _context.Students.Add(student);
            if (_context.SaveChanges() > 0){return student.Id;}else { return 0; }
        }

        public bool DeleteStudent(Student   student)
        {
            _context.Entry(student).Property("IsActive").IsModified =false;
            return _context.SaveChanges() > 0;
        }

        public Student GetStudentByEmail(string email)
        {
            return _context.Students.FirstOrDefault(x => x.Email == email);
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        public Student GetStudentrollnoClasswise(string cls, DateTime enrollmentdate)
        {
           var rollno= _context.Students.Where(x=>x.IsActive == true && x.Class == cls && x.DateOfAdmission.Year == enrollmentdate.Year).OrderByDescending(x=>x.RollNo).FirstOrDefault();
            return rollno;
            //&& x.DateOfAdmission.Year == enrollmentdate.Year
        }
        #endregion
        #region Methods
        public IEnumerable<Student> GetStudents() {
            return _context.Students.Where(x=>x.IsActive==true).ToList();
        }

        public bool UpdateStudent(Student student)
        {
           _context.Students.Update(student);
            return _context.SaveChanges() > 0;
        }
        #endregion



    }
}
