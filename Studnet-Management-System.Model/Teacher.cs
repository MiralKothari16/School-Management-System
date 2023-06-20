using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(230)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(5)]
        public string Class { get; set; }

        [MaxLength(25)]
        public string Subject { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateofBirth { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime EnrollmentDate { get; set; }

        [MaxLength(50)]
        public string Qualification { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("TeacherModifiedBy")]
        public int? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public virtual Admin TeacherModifiedBy { get; set; }
    }
}
