using BackEnd.DTO.User;
using BackEnd.Entities;

namespace BackEnd.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(CreateUserDto userDto);
        Task DeleteAsync(int userId);
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity?> GetByIdAsync(int id);
        Task<UserEntity?> GetByIdWithOrdersAsync(int userId);
        Task UpdateAsync(int userId, UpdateUserDto userDto);
        Task UpdatePasswordAsync(UpdateUserPasswordDto userDto);
    }
}