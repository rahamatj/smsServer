using Microsoft.AspNetCore.Mvc;
using smsServer.DTOs;
using smsServer.Entities;

namespace smsServer.Services;

public interface IAdminService
{
    Task<List<AdminDto>> GetAllAdmins();
    Task<User?> AddAdminAsync(UserDTO userDto);
    Task<bool> DoesUsernameExistAsync(string username);
    Task<User> EditAdminAsync(Guid id);
    Task<User> UpdateAdminAsync(AdminDto adminDto);
    Task<bool> UpdateAdminPasswordAsync(ChangePasswordDto changePasswordDto);
    Task<bool> DeleteAdminAsync(Guid id);
}