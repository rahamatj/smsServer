using Microsoft.AspNetCore.Mvc;
using smsServer.DTOs;
using smsServer.Entities;

namespace smsServer.Services;

public interface IAdminService
{
    Task<List<AdminDto>> GetAllAdmins();
    Task<User> AddAdminAsync(UserDTO userDto);
    Task<bool> DoesUsernameExistAsync(string username);
}