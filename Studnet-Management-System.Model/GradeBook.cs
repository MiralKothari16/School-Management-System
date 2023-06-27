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
    public class GradeBook
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("teacherId")]
        public int teacherId { get; set; }
        [ForeignKey("studentId")]
        public int studentId { get; set; }
        
        [MaxLength(25)]
        public string Subject { get; set; }
       
        [MaxLength(5)]
        public string Class { get; set; }

        [MaxLength(5)]
        public int Marks { get; set; }
        [MaxLength(5)]
        public int TotalMarks { get; set; }

      /// <summary>
      ///  public decimal Percentage { get; set;  }
      /// </summary>

        [Column(TypeName = "date")]
        public DateTime ExamDate { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    }
}
