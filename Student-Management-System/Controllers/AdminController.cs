using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Student_Management_System.Service.Interface;
using Studnet_Management_System.Model;
using System.Security.Claims;

namespace Student_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public ActionResult<GetAdminDTO> GetAdmins()
        {
            return Ok(_adminService.GetAdmins());
        }
        [HttpGet("Attendence")]
        public ActionResult<GetAdminDTO> GetStudentsAttendence()
        {
            return Ok(_adminService.GetStudentsAttendence());
        }
        [HttpPost]
        public IActionResult AddAdmin(AddAdminDTO admin)
        {
            return Ok(_adminService.AddAdmin(admin));
        }
        [HttpPut]
        public IActionResult UpdateAdmin(UpdateAdminDTO admin)
        {
            return Ok(_adminService.UpdateAdmin( admin));
        }

        //public IActionResult UpdateAdmin(UpdateAdminDTO admin)
        //{
        //    return Ok(_adminService.UpdateAdmin(admin));
        //}
        [HttpDelete]
        public ActionResult DeleteAdmin(int id)
        {
            return Ok(_adminService.DeleteAdmin(id));
        }
    }
}
