using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studnet_Management_System.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        [Column (TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }  
        public bool IsActive { get; set; }  
    }
}