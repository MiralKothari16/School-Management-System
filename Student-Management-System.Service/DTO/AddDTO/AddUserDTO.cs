using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.AddDTO
{
    public class AddUserDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is Invalid.")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password should be minimum {2} to maximum {1} characters long.")]
        public string Password { get; set; }

        public bool IsActive { get; set; } = true;
        public int RegisterrdId { get; set; }
    }
}
