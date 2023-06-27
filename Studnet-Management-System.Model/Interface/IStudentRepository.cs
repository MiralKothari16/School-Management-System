using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();

        int AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(Student student);

        Student GetStudentByEmail(string email);

        Student GetStudentrollnoClasswise(string cls, DateTime enrollmentdate);

        Student  GetStudentById(int id);   
    }
}
