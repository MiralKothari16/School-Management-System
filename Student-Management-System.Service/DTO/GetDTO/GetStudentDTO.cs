﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO.GetDTO
{
    public  class GetStudentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string RollNo { get; set;}
        public string Class { get; set;}
        public DateTime DateofBirth { get; set; }
        public DateTime DateofAdmission { get; set; }
        

    }
}
