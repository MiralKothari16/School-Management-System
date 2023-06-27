using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.AddDTO
{
    public class AddGradeBookDTO
    {
        [Required(ErrorMessage = "TeacherId is required")]
        public int teacherId { get; set; }
        [Required(ErrorMessage = "StudentId is required")]
        public int studentId { get; set; }
        [Required(ErrorMessage = "Subjcet is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }
        [Required(ErrorMessage = "Presence should not be blank")]
        public int Marks { get; set; }
        public int TotalMarks { get; set; }
        
        public DateTime ExamDate { get; set; }
    }
}
