using Microsoft.AspNetCore.Mvc;
using smsServer.DTOs;
using smsServer.Entities;
using smsServer.Services;

namespace smsServer.Controllers;

[Route("api/users")]
[ApiController]
public class AdminController(IAdminService adminService) : ControllerBase
{
    [HttpGet("admins")]
    public async Task<List<AdminDto>> GetAllAdmins()
    {
        return await adminService.GetAllAdmins();
    }

    [HttpPost("new")]
    public Task<User> AddAdmin([FromBody] UserDTO userDto)
    {
        return adminService.AddAdminAsync(userDto);
    }

    [HttpGet("does-username-exist")]
    public async Task<bool> DoesUsernameExist([FromQuery] string username)
    {
        return await adminService.DoesUsernameExistAsync(username);
    }
}