using Microsoft.EntityFrameworkCore;
using smsServer.Entities;
using smsServer.Enums;

namespace smsServer.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Username = "SuperAdmin",
                    PasswordHash = "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC",
                    Role = 0,
                },
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    Username = "Admin1",
                    PasswordHash = "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC",
                    Role = 1,
                },
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    Username = "Admin2",
                    PasswordHash = "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC",
                    Role = 2,
                },
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                    Username = "Admin3",
                    PasswordHash = "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC",
                    Role = 3,
                },
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                    Username = "Admin4",
                    PasswordHash = "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC",
                    Role = 4,
                },
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                    Username = "Admin5",
                    PasswordHash = "$2a$11$6umEUbykUoOCvpMzMjBlIOePB/tpmAMsU65dr2V/M/Pl7OEzgy2VC",
                    Role = 5,
                }
            );
        }
    }
}
