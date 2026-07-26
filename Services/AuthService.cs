using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using smsServer.Data;
using smsServer.DTOs;
using smsServer.Entities;
using System.Security.Claims;
using System.Text;

namespace smsServer.Services
{
    public class AuthService(ApplicationDbContext dbContext, IConfiguration configuration) : IAuthService
    {
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

        public async Task<string?> LoginAsync(UserDTO userDTO)
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

            var token = CreateToken(user);  // Pass 'user' instead of 'userDTO'

            return token;
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


    }
}
