using Microsoft.Identity.Client;
using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Studnet_Management_System.Model.Repository
{
    public class AttendanceRepository :IAttendenceRepository
    {
        #region Field
        private readonly StudMgtContext _context;
        #endregion
        #region Constructor
        public AttendanceRepository(StudMgtContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        public void AddAttendence(Attendence attendence)
        {
            _context.Attendence.Add(attendence);
            _context.SaveChanges();
            //if (_context.SaveChanges() > 0) { return student.Id; } else { return 0; }
        }
        //teacher or students  can get 
        public IEnumerable<object> GetAttByTeacherorStudentId(int id , int cyear)
        {
            var attendence = (from st in _context.Students join att in _context.Attendence
                              on st.Id equals att.studentId
                              where att.teacherId.Equals(id) && att.Currentdate.Year == cyear
                              select new
                              {
                                  Id = st.Id,
                                  Name = st.Name,
                                  Date=att.Currentdate,
                                  teacherId=att.teacherId,
                                  subject =att.Subject,
                                  presence = att.Presence,
                              }).ToList();
            if (attendence.Count > 0){ return attendence; } else { return null; }  
        }

        public IEnumerable<object> GetStudentAttendencesubjwise(int id, int cyear, string subject)
        {
            var attendence = (from st in _context.Students join att in _context.Attendence
                              on st.Id equals att.studentId
                              where att.studentId.Equals(id) && att.Currentdate.Year == cyear && att.Subject == subject
                              select new
                              {
                                  Id = st.Id,
                                  Name = st.Name,
                                  Date = att.Currentdate,
                                  teacherId = att.teacherId,
                                  subject = att.Subject,
                                  presence = att.Presence,
                                  Class=att.Class,
                              }).ToList();
            if (attendence.Count > 0) { return attendence; } else { return null; }
        }

        //admin can get all student attendence
        public IEnumerable<object> GetStudentsAttendence()
        {
            var attendence = (from st in _context.Students
                              join att in _context.Attendence
                              on st.Id equals att.studentId
                              where  st.IsActive==true
                              orderby att.Class
                              select new
                              {
                                  Id = st.Id,
                                  Name = st.Name,
                                  Date = att.Currentdate,
                                  teacherId = att.teacherId,
                                  subject = att.Subject,
                                  presence = att.Presence,
                                  Class = att.Class,    
                              }).ToList();
            if (attendence.Count > 0) { return attendence; } else { return null; }
        }
        //check todays attendence should not be added twice
        public bool IsPresentToday(int teacherid, int studentid, DateTime attendencedate)
        {
            var res = _context.Attendence.FirstOrDefault(x => x.teacherId == teacherid && x.studentId == studentid && x.Currentdate== attendencedate);
            if (res != null) return true; else return false;
        }

        //admin will update attendence
        public bool UpdateAttendene(Attendence attendence)
        {
            _context.Attendence.Update(attendence);
            return _context.SaveChanges() > 0;
        }

        //IEnumerable<Attendence> IAttendenceRepository.GetAttByTeacherorStudentId(int id, int year)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion


    }
}
