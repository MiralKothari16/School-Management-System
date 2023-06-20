using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.Interface;
using Student_Management_System.Service.Services;

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
        public ActionResult<GetStudentDTO> GetStudents()
        {
            return Ok(_studentService.GetStudents());
        }
        [HttpPost]
        public IActionResult AddStudent(AddStudentDTO student)
        {
            return Ok(_studentService.AddStudent(student));
        }
    }
}
