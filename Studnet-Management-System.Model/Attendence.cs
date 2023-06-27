using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model
{
    public class Attendence
    {
        [Key]
        public int  Id { get; set; }
        [ForeignKey ("teacherId")]
        public int teacherId { get; set; }
        [ForeignKey ("studentId")]
        public int studentId { get; set; }
        [MaxLength(25)]
        public string Subject { get; set; }
        [MaxLength(5)]
        public string Class { get; set; }
    
        public bool Presence { get; set; }

        [Column (TypeName ="date")]
        public DateTime Currentdate { get; set; }

       public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    
    }
}
