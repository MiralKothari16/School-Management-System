using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Interface
{
    public interface ITeacherService
    {
        ResponseDTO GetTeachers();
        // ResponseDTO UserPagination(int page, int content);
        ResponseDTO AddTeacher(AddTeacherDTO teacher);
        ResponseDTO UpdateTeacher(UpdateTeacherDTO teacher);
        ResponseDTO DeleteTeacher(int Id);
        ResponseDTO GetTeacherById(int Id);
        ResponseDTO GetTeacherByEmail(string Email);
    }
}
