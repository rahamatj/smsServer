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
        var admins = await dbContext.Users.Where(u => u.Role == (int)UserRole.Admin).ToListAsync();

        var adminDtos = admins.Select<User, AdminDto>(a => new AdminDto
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
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        return user;
    }
}