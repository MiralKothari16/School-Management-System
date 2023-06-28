using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface IGradeBookRepository
    {
        //for admin
        IEnumerable<object> GetStudentsMarks();

        void AddMarks(GradeBook gradbook);
        bool UpdateMarks(GradeBook  gradebook);

        //for teacher
        IEnumerable<object> GetMarksByTeacherId(int id , int year);
        // student see att subjectwies
        IEnumerable<object> GetStudentMakssubjwise(int Id, string subject,DateTime examdate);

        bool Checkmarksexamwise(int Id, DateTime examdt);

        bool Checkmarkssubjectwise(int Id, string subject);

        bool IsMarksAdded(int teacherid, int studentId, DateTime date,string subject);
    }
}
