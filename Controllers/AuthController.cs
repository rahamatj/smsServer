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
        public async Task<ActionResult<User>> Login(UserDTO userDTO)
        {
            var result = await authService.LoginAsync(userDTO);
            var user = await authService.GetUserByUsernameAsync(userDTO.Username);

            if (result is null || user is null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var response = new LoginResponseDTO
            {
                User = new LoggedInUserDTO { UserId = user.Id, Username = user.Username, Role = user.Role },
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            };

            return Ok(response);

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

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponseDTO>> RefreshTokens(RefreshTokenRequestDTO refreshTokenRequestDto)
        {
            var result = await authService.RefreshTokensAsync(refreshTokenRequestDto);

            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            return Ok(result);
        }

        [HttpGet("RefreshTokenGet")]
        public async Task<ActionResult<TokenResponseDTO>> RefreshTokensGet()
        {
            return Unauthorized("Invalid refresh token.");

        }
    }
}
