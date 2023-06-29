using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.AddDTO
{
    public  class AddTeacherDTO
    {
       // [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer than {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is Invalid.")]
        [MaxLength(30, ErrorMessage = "Email address should not be more than {1} characters long.")]
        public string Email { get; set; }

      //  [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password should be minimum {2} to maximum {1} characters long.")]
        public string Password { get; set; }

       // [Required(ErrorMessage = "Class is required")]
        [MaxLength(5, ErrorMessage = "Class can not be longer than {1} characters.")]
        public string Class { get; set; }

        //[Required(ErrorMessage = "Subject is required")]
        [MaxLength(25, ErrorMessage = "Subject can not be longer than {1} characters.")]
        public string Subject { get; set; }

       // [Required(ErrorMessage = "DateofBirth is required")]
        [Column(TypeName = "date")]
        public DateTime DateofBirth { get; set; }

        //[Required(ErrorMessage = "DateOfEnrollment is required")]
        [Column(TypeName = "datetime")]
        public DateTime EnrollmentDate { get; set; }

      //  [Required(ErrorMessage = "Qualification is required")]
        [MaxLength(50, ErrorMessage = "Qualification can not be longer than {1} characters.")]
        public string Qualification { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
