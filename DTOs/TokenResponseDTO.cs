using smsServer.Entities;

namespace smsServer.DTOs
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public User user { get; set; } = new();
    }
}
