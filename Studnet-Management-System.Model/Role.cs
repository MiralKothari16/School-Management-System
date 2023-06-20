using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string RoleName { get; set; }

    }
}
