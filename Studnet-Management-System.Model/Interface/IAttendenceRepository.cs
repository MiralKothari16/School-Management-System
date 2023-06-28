using Studnet_Management_System.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface IAttendenceRepository
    {
        // for admin   insert year wise
        IEnumerable<object> GetStudentsAttendence();

        void AddAttendence(Attendence attendence);
        bool UpdateAttendene(Attendence attendence);
        //teacher get attendence
        IEnumerable<object> GetAttByTeacherId(int id ,int  year);
        // student see att subjectwies
        IEnumerable<object> GetStudentAttendencesubjwise(int Id, int year,string subject);

        bool CheckattYearwise(int Id, int year);

        bool Checkattsubjectwise(int Id, string subject);

        bool IsPresentToday(int teacherid,int studentId,DateTime date);
    }
}
