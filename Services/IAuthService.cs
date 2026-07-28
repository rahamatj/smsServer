using smsServer.DTOs;
using smsServer.Entities;

namespace smsServer.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO userDto);
        Task<TokenResponseDTO?> LoginAsync(UserDTO userDto);
        Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO refreshTokenRequestDto);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
