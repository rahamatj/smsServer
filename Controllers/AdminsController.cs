using Microsoft.AspNetCore.Mvc;
using smsServer.DTOs;
using smsServer.Entities;
using smsServer.Services;

namespace smsServer.Controllers;

[Route("api/admins")]
[ApiController]
public class AdminsController(IAdminService adminService) : ControllerBase
{
    [HttpGet("")]
    public async Task<List<AdminDto>> GetAllAdmins()
    {
        return await adminService.GetAllAdmins();
    }

    [HttpPost("create")]
    public Task<User> AddAdmin([FromBody] UserDTO userDto)
    {
        return adminService.AddAdminAsync(userDto);
    }

    [HttpGet("does-username-exist/{username}")]
    public async Task<bool> DoesUsernameExist([FromRoute] string username)
    {
        return await adminService.DoesUsernameExistAsync(username);
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<User> EditAdminAsync([FromRoute] Guid id)
    {
        return await adminService.EditAdminAsync(id);
    }
    
    [HttpPut("update")]
    public async Task<User> UpdateAdminAsync(AdminDto adminDto)
    {
        return await adminService.UpdateAdminAsync(adminDto);
    }

    [HttpPatch("update-password")]
    public async Task<bool> UpdateAdminPasswordAsync([FromBody] ChangePasswordDto changePasswordDto)
    {
        return await adminService.UpdateAdminPasswordAsync(changePasswordDto);
    }
    
    [HttpDelete("delete/{id:guid}")]
    public async Task<bool> DeleteAdminAsync([FromRoute] Guid id)
    {
        return await adminService.DeleteAdminAsync(id);
    }
}