using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
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
        [Authorize(Roles = "Admin")]
        public ActionResult<GetTeacherDTO> GetTeachers()
        {
            return Ok(_teacherService.GetTeachers());
        }
        
        [HttpGet("id")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<GetTeacherDTO> GetTeacherById(int id)
        {
            return Ok(_teacherService.GetTeacherById(id));
        }

        [HttpGet("Attendence")]
        public ActionResult<object> GetStudentAttendence(int id, int year)
        {
            return Ok(_teacherService.GetAttByTeacherId(id, year));
        }

        [HttpGet("Mrk")]
        public ActionResult<object> GetStudentMarks(int id, int year)
        {
            return Ok(_teacherService.GetMarksTeacherIdwise(id, year));
        }


        [HttpPost]
        //[AllowAnonymous]
        public IActionResult AddTeacher(AddTeacherDTO teacher)
        {
            return Ok(_teacherService.AddTeacher(teacher));
        }
        
        [HttpPost("att")]
        public IActionResult Addattendence(AddAttendenceDTO attendence)
        {
            return Ok(_teacherService.AddAttendence(attendence));
        }

        [HttpPost("Mark")]
        public IActionResult AddMarks(AddGradeBookDTO gradebook)
        {
            return Ok(_teacherService.AddMarks(gradebook));
        }

        
        [HttpPut("teacher")]
       [Authorize(Roles = "Admin")]
        public IActionResult UpdateTeacher(UpdateTeacherDTO teacher)
        {
            return Ok(_teacherService.UpdateTeacher(teacher));
        }

        [HttpPut("attendence")]
        public IActionResult UpdateAttendence(UpdateAttendenceDTO attendence)
        {
            return Ok(_teacherService.UpdateAttendence(attendence));
        }
        [HttpPut("marks")]
        public IActionResult UpdateMarks(UpdateGradeBookDTO gradebook)
        {
            return Ok(_teacherService.UpdateMarks(gradebook));
        }
    }
}
