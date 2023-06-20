using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string RollNo { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(5)]
        public string Class { get; set; }

        [Column (TypeName ="date") ]
        public DateTime DateofBirth { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DateOfAdmission { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("StudntModifiedBy")]
        public int? ModifiedBy { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? ModifiedOn { get; set; }

        public virtual Admin StudntModifiedBy { get; set; }
    }
}
