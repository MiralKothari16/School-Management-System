using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.DTO
{
    public class TokenDTO
    {
        public TokenDTO()
        {
            Refresh_Token = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
        }
        public string Grant_Type { get; set; } = "password";

        public string Refresh_Token { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
