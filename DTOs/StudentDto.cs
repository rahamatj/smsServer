using smsServer.Enums;

namespace smsServer.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}