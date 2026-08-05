using Microsoft.EntityFrameworkCore;
using smsServer.Data;
using smsServer.DTOs;
using smsServer.Entities;
using smsServer.Enums;

namespace smsServer.Services;

public class StudentService(ApplicationDbContext dbContext) : IStudentService
{
    private const int StudentRole = (int)UserRole.Student;

    public async Task<List<StudentDto>> GetAllStudents()
    {
        return await dbContext
            .Users
            .Where(s => s.Role == StudentRole)
            .OrderBy(a => a.CreatedOn)
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Username = s.Username,
                Role = (UserRole)s.Role,
            })
            .ToListAsync();
    }

    public async Task<User> AddStudentAsync(UserDTO userDto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = userDto.Username,
            PasswordHash = hashedPassword,
            Role = StudentRole,
            CreatedOn = DateTime.UtcNow,
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        return user;
    }
    
    public async Task<bool> DoesUsernameExistAsync(string username)
    {
        return await dbContext.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<User> EditStudentAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Role != StudentRole)
        {
            throw new Exception("Student not found");
        }
        
        return user;
    }

    public async Task<User> UpdateStudentAsync(StudentDto studentDto)
    {
        var user = await dbContext.Users.FindAsync(studentDto.Id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Role != StudentRole)
        {
            throw new Exception("Student not found");
        }
        
        user.Username = studentDto.Username;
        user.Role = StudentRole;
        
        await dbContext.SaveChangesAsync();
        
        return user;
    }
    
    public async Task<bool> UpdateStudentPasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var user = await dbContext.Users.FindAsync(changePasswordDto.Id);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.Password);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        user.PasswordHash = hashedPassword;
        await dbContext.SaveChangesAsync();
        
        return user != null;
    }

    public async Task<bool> DeleteStudentAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Role != StudentRole)
        {
            throw new Exception("Student not found");
        }
        
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        
        return user != null;
    }
}