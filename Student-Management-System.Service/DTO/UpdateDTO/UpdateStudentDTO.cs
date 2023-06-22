using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.UpdateDTO
{
    public  class UpdateStudentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name can not be longer than {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RollNo is required")]
        [MaxLength(10, ErrorMessage = "RollNo can not be longer than {1} characters.")]
        public string RollNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is Invalid.")]
        [MaxLength(30, ErrorMessage = "Email address should not be more than {1} characters long.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password should be minimum {2} to maximum {1} characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [MaxLength(5, ErrorMessage = "Class can not be longer than {1} characters.")]
        public string Class { get; set; }

        [Required(ErrorMessage = "DateofBirth is required")]
        [Column(TypeName = "date")]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "DateOfAdmission is required")]
        [Column(TypeName = "datetime")]
        public DateTime DateOfAdmission { get; set; }

       // public bool IsActive { get; set; } 

        public int? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
    }
}
