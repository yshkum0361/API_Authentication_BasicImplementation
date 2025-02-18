using API_Authentication_BasicImplementation.Entities;
using API_Authentication_BasicImplementation.Models;
using API_Authentication_BasicImplementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Authentication_BasicImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]

        public async Task<ActionResult<User>> Register(UserDTO request)
        {

            var user  = await authService.RegisterAsync(request);

            if (user == null)
            {
                return BadRequest("User Already Exist!!!");
            }
            return Ok(user);

        }

        [HttpPost("login")]

        public async Task<ActionResult<TokenResponseDTO>> Login(UserDTO request)
        {
           var token = await authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Invalid Username or Password!!!!");
            }
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndPoint()
        {
            return Ok("You Are Authenticated.");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("AdminOnly")]
        public IActionResult AdminOnlyEndPoint()
        {
            return Ok("You Are An Admin.");
        }

        [HttpPost]

        public async Task<ActionResult<TokenResponseDTO>> RefreshToken(RefreshTokenRequestDTO request)
        {
            var result  = await authService.RefreshTokenAsync(request);
            if(result == null || result.AccessToken is null || result.RefreshToken is null )
            {
                return Unauthorized("Invalid Refresh Token!!!");
            }
            return Ok(result);  
        }

    }
}
