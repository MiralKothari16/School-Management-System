using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model
{
    public class RToken
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Refresh_Token { get; set; }
        public int Is_Stop { get; set; }

        [Column (TypeName ="datetime")]
        public DateTime Created_Date { get; set; }

        //public int User_Id { get; set; }// column in table 
       
       [Column("User_Id")]//  column name in sqltable.
        public int UserId { get; set; } //use for DTO

        [Column("UserId")]
        public User UserNavigation { get; set; }


    }
}
