using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.Interface;

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
        public ActionResult<GetAdminDTO> GetAdmin()
        {
            return Ok(_adminService.GetAdmins);
        }
        [HttpPost]
        public IActionResult AddAdmin(AddAdminDTO admin)
        {
            return Ok(_adminService.AddAdmin(admin));
        }
    }
}
