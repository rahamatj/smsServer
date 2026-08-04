using System.ComponentModel.DataAnnotations;

namespace smsServer.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        public int Role { get; set; }

        [MaxLength(512)]
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
