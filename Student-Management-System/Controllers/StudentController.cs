using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.Interface;
using Student_Management_System.Service.Services;
using Studnet_Management_System.Model;
using System.Data;

namespace Student_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) { 
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<GetStudentDTO> GetStudents()
            {
            return Ok(_studentService.GetStudents());
        }
      
        [HttpGet("GetAttendence")]
        public ActionResult<GetStudentDTO> GetStudentsAttendence(int id, int year,string subject)
        {
            return Ok(_studentService.GetStudentAttendencesubjwise(id,year,subject));
        }
        [HttpGet ("GetStudentById")]
        public ActionResult<GetStudentDTO> GetStudentById(int id)
        {
            return Ok(_studentService.GetStudentById(id));
        }

        [HttpGet("GetMarks")]
        public ActionResult<GetStudentDTO> GetStudentsMarks(int id,string subject,DateTime examdate)
        {
            return Ok(_studentService.GetStudentMarkssubjwise(id, subject,examdate));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddStudent(AddStudentDTO student)
        {
            return Ok(_studentService.AddStudent(student));
        }
       
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateStudent (UpdateStudentDTO student)
        {
            return Ok(_studentService.UpdateStudent(student));
        }
    }
}
