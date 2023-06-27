using Studnet_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Interface
{
    public interface IRTokenService
    {
        bool AddToken(RToken token);

        bool ExpireToken(RToken token);

        RToken GetToken(string refreshToken);
    }
}
