using BackEnd.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.DTO.User;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ShopDbContext _context;
        public UsersRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByIdWithOrdersAsync(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> GetByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        // Uncomment this method if you need to retrieve a user by their name
        /*public async Task<UserEntity?> GetByNameAsync(string userName)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == userName);
        }*/

        public async Task AddAsync(CreateUserDto userDto)
        {
            var user = new UserEntity
            {
                Name = userDto.Name,
                Password = userDto.Password,
                Address = userDto.Address,
                Email = userDto.Email
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int userId, UpdateUserDto userDto)
        {
            await _context.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(x => x.Name, userDto.Name)
                    .SetProperty(x => x.Address, userDto.Address)
                    .SetProperty(x => x.Email, userDto.Email));
        }

        public async Task UpdatePasswordAsync(UpdateUserPasswordDto userDto)
        {
            await _context.Users
                .Where(u => u.Id == userDto.Id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(x => x.Password, userDto.NewPassword));
        }

        public async Task DeleteAsync(int userId)
        {
            await _context.Users
                .Where(u => u.Id == userId)
                .ExecuteDeleteAsync();
        }
    }
}
