using smsServer.Enums;

namespace smsServer.DTOs;

public class AdminDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public int Role { get; set; }
}