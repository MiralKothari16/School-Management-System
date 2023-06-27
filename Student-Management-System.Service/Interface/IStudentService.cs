using Student_Management_System.Service.DTO;
using Student_Management_System.Service.DTO.AddDTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.DTO.GetDTO;

namespace Student_Management_System.Service.Interface
{
    public interface IStudentService
    {
        ResponseDTO GetStudents();
       // ResponseDTO UserPagination(int page, int content);
        ResponseDTO AddStudent(AddStudentDTO student);
        ResponseDTO UpdateStudent(UpdateStudentDTO student);
        ResponseDTO DeleteStudent(int Id);
        ResponseDTO GetStudentById(int Id);
        ResponseDTO GetStudentByEmail(string Email);
        // use for authentication

        ResponseDTO GetStudentAttendencesubjwise(int id, int year, string subject);

        // check again 
        /////GetStudentDTO IsStudentExists(TokenDTO model);
    }
}
