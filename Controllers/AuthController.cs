using Microsoft.AspNetCore.Mvc;
using smsServer.DTOs;
using smsServer.Entities;
using smsServer.Services;

namespace smsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterAsync(UserDTO userDTO)
        {
            var user = await authService.RegisterAsync(userDTO);

            if (user is null)
            {
                return Conflict("Username already exists.");
            }

            return StatusCode(201, new { message = "User registered successfully", user });
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login(UserDTO userDTO)
        {
            var result = await authService.LoginAsync(userDTO);

            if (result is null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(result);

        }

        [HttpGet]
        public ActionResult AuthenticatedOnlyEndpoint()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Ok(new { message = "User is authenticated" });
            }

            return Unauthorized(new { message = "User is not authenticated" });
        }

        [HttpGet("Admin")]
        public ActionResult AdminOnlyEndpoint()
        {

            if (User.IsInRole("Admin"))
            {
                return Ok(new { message = "Admin is authorized" });
            }

            return Unauthorized(new { message = "Admin is not authorized" });
        }

    }
}
