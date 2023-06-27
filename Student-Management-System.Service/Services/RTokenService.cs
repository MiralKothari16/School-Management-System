using AutoMapper;
using Studnet_Management_System.Model.Interface;
using Studnet_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management_System.Service.Interface;

namespace Student_Management_System.Service.Services
{
    public class RTokenService :IRTokenService
    {
        #region Fields
        private readonly IRTokenRepository _rtokenRepository;
        private readonly IMapper _mapper;
        #endregion
        
        #region Constructor
        public RTokenService(IRTokenRepository rtokenRepository, IMapper mapper)
        {
            _rtokenRepository = rtokenRepository;
            _mapper = mapper;
        }
        #endregion
        #region Method      
        public bool AddToken(RToken token)
        {
            //   return _rtokenRepository.Add(token);
            return _rtokenRepository.Add(_mapper.Map<RToken>(token));
        }
        public bool ExpireToken(RToken token)
        {
            // return _rtokenRepository.Expire(token);
            return _rtokenRepository.Expire(_mapper.Map<RToken>(token));
        }
        public RToken GetToken(string refreshToken)
        {
            return _rtokenRepository.Get(refreshToken);
        }
        #endregion
    }
}
