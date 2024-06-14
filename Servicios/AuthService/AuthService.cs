using Commons.DTOs;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.DBContext;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly string secretKey;

        private readonly IRepository<User> userRepository;

        public AuthService(IRepository<User> userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;

            //secretKey = config.GetSection("settings").GetSection("secretKey").ToString();

        }

        public async Task<Response<User>> Authenticate(AuthRequest userReq)
        {

            var response = new Response<User>();

            try
            {

                Func<User, bool> predicate = u => u.UserName == userReq.UserName && u.Password == userReq.Password;

                var userAuth = await userRepository.GetByEntity(predicate);

                if (userAuth != null)
                {
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.Name, userAuth.UserName));
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userAuth.Id.ToString()));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(120),
                        //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokencreated = tokenHandler.WriteToken(tokenConfig);

                    response.message = tokencreated;
                    response.data = userAuth;
                    response.status = 200;
                }
                else
                {
                    response.message = "Usuario invalido";
                    response.status = 401;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }

            return response;
        }
    }
}
