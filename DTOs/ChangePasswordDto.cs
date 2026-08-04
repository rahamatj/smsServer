namespace smsServer.DTOs;

public class ChangePasswordDto
{
    public Guid Id { get; set; }
    public required string Password { get; set; }
}