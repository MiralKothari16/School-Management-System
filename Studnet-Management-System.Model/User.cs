using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Password { get; set; }

        public bool IsActive    { get; set; }
        [ForeignKey("roleId")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }  


      //  public virtual Students adminstudteacherId { get; set; }
       // public virtual Teachers adminstudteacherId { get; set; }
      //  public virtual Role Role { get; set; }
    }
}
