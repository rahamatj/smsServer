using smsServer.Enums;

namespace smsServer.DTOs
{
    public class LoggedInUserDTO
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
