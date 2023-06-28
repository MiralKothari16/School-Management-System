using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Student_Management_System.Service.DTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.Interface;
using Studnet_Management_System.Model;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Student_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region fields
        private readonly IUserService _userService;
        private readonly IRTokenService _rtokenService;
        private IOptions<JWTConfigDTO> _jwtConfig;
        #endregion
        #region constructor
        public TokenController(IUserService userService, IRTokenService rtokenService, IOptions<JWTConfigDTO> jwtConfig)
        {
            _userService = userService;
            _rtokenService = rtokenService;
            _jwtConfig = jwtConfig;
            //_userRoleService = userRoleService;
        }
        #endregion
        #region Methods
        [HttpPost]
        [Route("Auth")]
        public void Auth([FromBody] TokenDTO parameters)
        {
            switch (parameters.Grant_Type)
            {
                case "password" when string.IsNullOrEmpty(parameters.Username):
                    throw new Exception("Enter Username.");

                case "password" when string.IsNullOrEmpty(parameters.Username) && string.IsNullOrEmpty(parameters.Password):
                    throw new Exception("Enter Username and Password.");
                case "password" when !string.IsNullOrEmpty(parameters.Username) && !string.IsNullOrEmpty(parameters.Password):
                    Login(parameters);
                    break;
                case "password" when string.IsNullOrEmpty(parameters.Username) && parameters.Password == "":
                    throw new Exception("Enter Password.");
                case "password":
                    throw new Exception("Something Went Wrong,Try Again.");
                case "Refresh_Token":
                    RefreshToken(parameters);
                    break;
                default:
                    throw new Exception("Invalid Grant_Type");
            }

            //if (string.IsNullOrEmpty(parameters.Username)){
                
            //    throw new Exception("Enter Username."); }
            //else if (string.IsNullOrEmpty(parameters.Username) && string.IsNullOrEmpty(parameters.Password))
            //        throw new Exception("Enter Username and Password.");
            //else if (!string.IsNullOrEmpty(parameters.Username) && !string.IsNullOrEmpty(parameters.Password))
            //      Login(parameters);
            //else if (string.IsNullOrEmpty(parameters.Username) && parameters.Password == "")
            //        throw new Exception("Enter Password.");
            //else { RefreshToken(parameters); }
        }

        private void Login(TokenDTO parameters)
        {
            var sessionModel = _userService.IsUserExists(parameters);
            if (sessionModel == null)

            {
                throw new Exception("Invalid Username or Password.");
            }
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            var rToken = new RToken
            {
                Refresh_Token = refreshToken,
                Is_Stop = 0,
                UserId = sessionModel.Id,
                Created_Date = DateTime.Now
            };
            if (_rtokenService.AddToken(rToken))
            {
                GetJWT(sessionModel, refreshToken);
            }
            else
            {
                throw new Exception("Faild to add Token.");
            }
        }

        private void RefreshToken(TokenDTO parameters)
        {
            var token = _rtokenService.GetToken(parameters.Refresh_Token);
            if (token == null)
            {
                throw new Exception("Can not refresh token.");
            }
            if (token.Is_Stop == 1)
            {
                throw new Exception("Refreshed token has expired.");
            }
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            token.Is_Stop = 1;
            //expire the old refresh token and add new refresh token

            var updateFlag = _rtokenService.ExpireToken(token);

            var addFlag = _rtokenService.AddToken(new RToken
            {
                Refresh_Token = refreshToken,
                Is_Stop = 0,
                UserId = token.UserId,
                Created_Date = DateTime.UtcNow
            });
            if (updateFlag && addFlag)
            {
                GetJWT(refreshToken);
            }
            else
            {
                throw new Exception("Can not expire Token or new token.");
            }
        }
        private async Task GetJWT(GetUserDTO sessionModel, string rereshToken)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,sessionModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64),
                new Claim("UserId",Convert.ToString(sessionModel.Id)),
                new Claim("UserName",Convert.ToString(sessionModel.Email))
             };
            var roles = _userService.GetRoles(sessionModel.Email);
            foreach (var role in roles)
            {
                claims.Add(new Claim("Role", role));
            }

            var symmetricKetAsBase64 = _jwtConfig.Value.SecretKey;
            var KeyByteArray = Encoding.ASCII.GetBytes(symmetricKetAsBase64);
            var signingkey = new SymmetricSecurityKey(KeyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig.Value.Issuer,
                audience: _jwtConfig.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromHours(24)),
                signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = sessionModel.Email ?? "",
                expires_in = (int)TimeSpan.FromHours(24).TotalSeconds,
                refresh_token = rereshToken,
                user_id = sessionModel.Id
            };
            Request.HttpContext.Response.ContentType = "application/json";
            await Request.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));

        }
        private void GetJWT(string refreshToken)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,refreshToken),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,now.ToUniversalTime().ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64)
            };
            var symmetricKeyAsBase64 = _jwtConfig.Value.SecretKey;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig.Value.Issuer,
                audience: _jwtConfig.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromHours(24)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expire_in = (int)TimeSpan.FromHours(24).TotalSeconds,
                refresh_token = refreshToken
            };
            Request.HttpContext.Response.ContentType = "application/json";
            Request.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
        #endregion

    }
}
