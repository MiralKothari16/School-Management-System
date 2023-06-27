using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO
{
    public class JWTConfigDTO
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }
        public string Aud { get; set; }
    }
}
