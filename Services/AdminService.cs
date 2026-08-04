using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smsServer.Data;
using smsServer.DTOs;
using smsServer.Entities;
using smsServer.Enums;

namespace smsServer.Services;

public class AdminService(ApplicationDbContext dbContext) : IAdminService
{
    public async Task<List<AdminDto>> GetAllAdmins()
    {
        var admins = await dbContext
            .Users
            .OrderBy(a => a.CreatedOn)
            .ToListAsync();

        var adminDtos = admins.Select(a => new AdminDto
        {
            Id = a.Id,
            Username = a.Username,
            Role = (int)a.Role,
        }).ToList();
        
        return adminDtos;
    }

    public async Task<User> AddAdminAsync(UserDTO userDto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = userDto.Username,
            PasswordHash = hashedPassword,
            Role = (int)userDto.Role,
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

    public async Task<User> EditAdminAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return user;
    }

    public async Task<User> UpdateAdminAsync(AdminDto adminDto)
    {
        var user = await dbContext.Users.FindAsync(adminDto.Id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        user.Username = adminDto.Username;
        user.Role = (int)adminDto.Role;
        
        await dbContext.SaveChangesAsync();
        
        return user;
    }
    
    public async Task<bool> UpdateAdminPasswordAsync(ChangePasswordDto changePasswordDto)
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

    public async Task<bool> DeleteAdminAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        
        return user != null;
    }
}