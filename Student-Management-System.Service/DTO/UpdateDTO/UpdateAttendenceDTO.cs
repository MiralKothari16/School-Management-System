using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.UpdateDTO
{
    public class UpdateAttendenceDTO
    {
        [Required(ErrorMessage = "TeacherId is required")]
        public int teacherId { get; set; }
        [Required(ErrorMessage = "StudentId is required")]
        public int studentId { get; set; }
            
        [Required(ErrorMessage = "Presence should not be blank")]
        public bool Presence { get; set; }
        public string subject { get; set; }
        public string Class { get; set; }

        public DateTime currentdate { get; set; }

    }
}
