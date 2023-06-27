using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface ITeacherRepository
    {
        IEnumerable<Teacher> GetTeachers();

        int AddTeacher(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool DeleteTeacher(Teacher teacher);
       
        Teacher GetTeacherByEmail(string email);

        Teacher GetTeacherById(int id);
        string GetSubject(string subj,int id);
        string GetClass(string cls,int id);

        string GetClassTeacher(string cls);
    }
}
