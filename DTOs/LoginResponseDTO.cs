namespace smsServer.DTOs
{
    public class LoginResponseDTO
    {
        public LoggedInUserDTO? User { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
