using BackEnd.DTO.User;
using BackEnd.Models;

namespace BackEnd.Interfaces.Services
{
    public interface IUsersService
    {
        Task<ServiceResult> CreateUserAsync(CreateUserDto user);
        Task<ServiceResult> DeleteUserAsync(int userId);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserWithOrdersDto?> GetUserByIdWithOrdersAsync(int userId);
        Task<ServiceResult<string>> LoginAsync(string email, string password);
        Task<ServiceResult> UpdateUserAsync(int id, UpdateUserDto userDto);
        Task<ServiceResult> UpdateUserPasswordAsync(int userId, string oldPassword, string newPassword);
    }
}