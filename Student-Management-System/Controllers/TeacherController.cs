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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) { 
            _teacherService = teacherService;
        }

        [HttpGet]
        public ActionResult<GetTeacherDTO> GetTeachers()
        {
            return Ok(_teacherService.GetTeachers());
        }
        [HttpPost]
        public IActionResult AddTeacher(AddTeacherDTO teacher)
        {
            return Ok(_teacherService.AddTeacher(teacher));
        }
    }
}
