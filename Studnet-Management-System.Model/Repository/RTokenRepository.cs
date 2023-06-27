using Studnet_Management_System.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studnet_Management_System.Model.Repository
{
    public class RTokenRepository :IRTokenRepository
    {
        private readonly StudMgtContext _context;

        public RTokenRepository(StudMgtContext context)
        {
            _context = context;
        }
        public RToken Get(string refreshToken)
        {
            return _context.RTokens.FirstOrDefault(predicate: x => x.Refresh_Token == refreshToken);
        }

        public bool Add(RToken token)
        {
            _context.RTokens.Add(token);
            return _context.SaveChanges() > 0;
        }

        public bool Expire(RToken token)
        {
            _context.RTokens.Update(token);
            return _context.SaveChanges() > 0;
        }
    }
}
