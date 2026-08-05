using Microsoft.AspNetCore.Mvc;
using smsServer.DTOs;
using smsServer.Entities;
using smsServer.Services;

namespace smsServer.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    [HttpGet("")]
    public async Task<List<StudentDto>> GetAllStudents()
    {
        return await studentService.GetAllStudents();
    }

    [HttpPost("create")]
    public Task<User> AddStudent([FromBody] UserDTO userDto)
    {
        return studentService.AddStudentAsync(userDto);
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<User> EditStudentAsync([FromRoute] Guid id)
    {
        return await studentService.EditStudentAsync(id);
    }
    
    [HttpPut("update")]
    public async Task<User> UpdateStudentAsync(StudentDto studentDto)
    {
        return await studentService.UpdateStudentAsync(studentDto);
    }

    [HttpPatch("update-password")]
    public async Task<bool> UpdateStudentPasswordAsync([FromBody] ChangePasswordDto changePasswordDto)
    {
        return await studentService.UpdateStudentPasswordAsync(changePasswordDto);
    }
    
    [HttpDelete("delete/{id:guid}")]
    public async Task<bool> DeleteStudentAsync([FromRoute] Guid id)
    {
        return await studentService.DeleteStudentAsync(id);
    }
}