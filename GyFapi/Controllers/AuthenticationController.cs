using Commons.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.AuthService;

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
        public async Task<ActionResult<Response<AuthResponse>>> Login(AuthRequest authRequest)
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
