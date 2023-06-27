using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Interface
{
    public interface IRTokenRepository
    {
        bool Add(RToken token);

        bool Expire(RToken token);

        RToken Get(string refreshToken);
    }
}
