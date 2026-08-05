using smsServer.DTOs;
using smsServer.Entities;

namespace smsServer.Services;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudents();
    Task<User> AddStudentAsync(UserDTO userDto);
    Task<User> EditStudentAsync(Guid id);
    Task<User> UpdateStudentAsync(StudentDto studentDto);
    Task<bool> UpdateStudentPasswordAsync(ChangePasswordDto changePasswordDto);
    Task<bool> DeleteStudentAsync(Guid id);
}