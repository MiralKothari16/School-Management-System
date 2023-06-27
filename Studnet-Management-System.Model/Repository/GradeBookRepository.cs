using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Repository
{
    public class GradeBookRepository :IGradeBookRepository
    {
        #region Field
        private readonly StudMgtContext _context;
        #endregion
        #region Constructor
        public GradeBookRepository(StudMgtContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public void AddMarks(GradeBook gradbook)
        {
            _context.GradeBooks.Add(gradbook);
            _context.SaveChanges();
        }

        //teacher or students  can get 
        public IEnumerable<object> GetMarksByTeacherId(int id, int cyear)
        {
            var result = (from st in _context.Students
                              join grd in _context.GradeBooks
                              on st.Id equals grd.studentId
                              where grd.teacherId.Equals(id) && grd.ExamDate.Year == cyear
                              select new
                              {
                                  Id = st.Id,
                                  Name = st.Name,
                                  Date = grd.ExamDate,
                                  teacherId = grd.teacherId,
                                  subject = grd.Subject,
                                  Marks = grd.Marks,
                                  totalMarks = grd.TotalMarks,
                                  Class = grd.Class,
                              }).ToList();
            if (result.Count > 0) { return result; } else { return null; }
        }

      
        public IEnumerable<object> GetStudentMakssubjwise(int Id, string subject, string cls)
        {
            var result = (from st in _context.Students
                              join grd in _context.GradeBooks
                                on st.Id equals grd.studentId
                              where grd.studentId.Equals(Id) && grd.Class == cls && grd.Subject == subject
                              select new
                              {
                                  Id = st.Id,
                                  Name = st.Name,
                                  Date = grd.ExamDate,
                                  teacherId = grd.teacherId,
                                  subject = grd.Subject,
                                  Marks = grd.Marks,
                                  totalMarks = grd.TotalMarks,
                                  Class = grd.Class,
                              }).ToList();
            if (result.Count > 0) { return result; } else { return null; }
        }

        //admin can get all student attendence
     
        public IEnumerable<object> GetStudentsMarks()
        {
            var result = (from st in _context.Students
                              join grd in _context.GradeBooks
                              on st.Id equals grd.studentId
                              where st.IsActive == true
                              orderby grd.Class
                              select new
                              {
                                  Id = st.Id,
                                  Name = st.Name,
                                  Date = grd.ExamDate,
                                  teacherId = grd.teacherId,
                                  subject = grd.Subject,
                                  Marks= grd.Marks,
                                  totalMarks = grd.TotalMarks,
                                  Class = grd.Class,
                              }).ToList();
            if (result.Count > 0) { return result; } else { return null; }
        }

        //check todays attendence should not be added twice
        public bool IsMarksAdded(int teacherid, int studentid, DateTime examdate,string sub)
        {
            var res = _context.GradeBooks.FirstOrDefault(x => x.teacherId == teacherid && x.studentId == studentid && x.ExamDate == examdate && x.Subject==sub);
            if (res != null) return true; else return false;
        }

        //admin will update attendence
        public bool UpdateMarks(GradeBook gradebook)
        {
            _context.GradeBooks.Update(gradebook);
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
