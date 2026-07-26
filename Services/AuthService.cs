using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using smsServer.Data;
using smsServer.DTOs;
using smsServer.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace smsServer.Services
{
    public class AuthService(ApplicationDbContext dbContext, IConfiguration configuration) : IAuthService
    {
        public async Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDto)
        {
            var user = await (refreshTokenRequestDto.UserId, refreshTokenRequestDto.RefreshToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set the refresh token expiry time to 7 days from now

            await dbContext.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(User user)  // Change parameter type from UserDTO to User
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Issuer = configuration.GetValue<string>("AppSettings:Issuer"),
                Audience = configuration.GetValue<string>("AppSettings:Audience")
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<TokenResponseDTO?> LoginAsync(UserDTO userDTO)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == userDTO.Username);

            if (user is null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(userDTO.Password, user.PasswordHash))
            {
                return null;
            }

            var response = new TokenResponseDTO
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };

            return response;
        }

        public async Task<User?> RegisterAsync(UserDTO userDTO)
        {
            if (await dbContext.Users.AnyAsync(u => u.Username == userDTO.Username))
            {
                return null;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            var user = new User
            {
                Username = userDTO.Username,
                PasswordHash = hashedPassword
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public Task<TokenResponseDTO?> ValidateRefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
