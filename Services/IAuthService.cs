using smsServer.DTOs;
using smsServer.Entities;

namespace smsServer.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO userDto);
        Task<string?> LoginAsync(UserDTO userDto);
    }
}
