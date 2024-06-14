using Commons.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repository;
using Services.AuthService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GyFapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        readonly IAuthService authService;

        public AuthenticationController( IAuthService authService)
        { 
            this.authService = authService;
                
        }
        
        [HttpPost]
        public async Task<ActionResult<Response<User>>> Login(AuthRequest authRequest)
        {
            ActionResult response;

            var userResponse = await authService.Authenticate(authRequest);

            if (userResponse.status == 200)
            { response = Ok(userResponse); }
            else
            { response = Unauthorized(userResponse); }

            return response;
        }

    }
}
