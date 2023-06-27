using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.GetDTO
{
    public class GetAttendenceDTO
    {
        public int teacherId { get; set; }
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string Subject { get; set; }
       /// <summary>
       /// public string Class { get; set; }
       /// </summary>
        public bool Presence { get; set; }
        public DateTime currentdate { get; set; }
    }
}
