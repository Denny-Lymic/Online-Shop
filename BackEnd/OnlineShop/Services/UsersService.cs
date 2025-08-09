using BackEnd.Interfaces.Repositories;
using BackEnd.Interfaces.Services;
using BackEnd.DTO.Order;
using BackEnd.DTO.User;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Repositories;
using System.Text.RegularExpressions;

namespace BackEnd.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtProvider _jwtProvider;

        public UsersService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            JwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<UserWithOrdersDto?> GetUserByIdWithOrdersAsync(int userId)
        {
            var user = await _usersRepository.GetByIdWithOrdersAsync(userId);

            if (user == null)
            {
                return null;
            }

            return new UserWithOrdersDto
            {
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                Orders = user.Orders.Select(o => new OrderShortDto
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.Status,
                    ProductId = o.ProductId,
                    ProductName = o.Product?.Name ?? "Unknown"
                }).ToList()
            };
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Name = user.Name,
                Address = user.Address,
                Email = user.Email
            };
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _usersRepository.GetByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllAsync();

            if (users == null)
            {
                return new List<UserDto>();
            }

            var usersDtos = new List<UserDto>();

            foreach (var user in users)
            {
                usersDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Address = user.Address,
                    Email = user.Email
                });
            }

            return usersDtos;
        }

        public async Task<ServiceResult> CreateUserAsync(CreateUserDto user)
        {
            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            var existingUser = await _usersRepository.GetByEmailAsync(user.Email);

            var result = new ServiceResult();

            if (!emailPattern.IsMatch(user.Email))
            {
                result.Errors.Add("Email is not valid.");
            }

            if (existingUser != null)
            {
                result.Errors.Add("Пользователь с таким email уже существует");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            var hashedPassword = new PasswordHasher().GenerateHash(user.Password); // In a real application, you would hash the password here

            var newUser = new CreateUserDto
            {
                Name = user.Name,
                Password = hashedPassword,
                Address = user.Address,
                Email = user.Email
            };

            await _usersRepository.AddAsync(newUser);

            return result;
        }

        public async Task<ServiceResult> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var result = new ServiceResult();

            var existingUser = await _usersRepository.GetByIdAsync(id);

            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (existingUser == null)
            {
                result.Errors.Add("User not found.");
                return result;
            }

            if (string.IsNullOrEmpty(userDto.Name))
            {
                userDto.Name = existingUser.Name;
            }
            if (string.IsNullOrEmpty(userDto.Address))
            {
                userDto.Address = existingUser.Address;
            }
            if (string.IsNullOrEmpty(userDto.Email))
            {
                userDto.Email = existingUser.Email;
            }

            if (!emailPattern.IsMatch(userDto.Email!))
            {
                result.Errors.Add("Email is not valid.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            await _usersRepository.UpdateAsync(id, userDto);

            return result;
        }

        public async Task<ServiceResult> UpdateUserPasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var result = new ServiceResult();

            var existingUser = await _usersRepository.GetByIdAsync(userId);

            if (existingUser == null)
            {
                result.Errors.Add("User not found.");
                return result;
            }

            var verify = _passwordHasher.VerifyHash(oldPassword, existingUser.Password);

            if (verify == false)
            {
                result.Errors.Add("Old password is incorrect.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            var hashedNewPassword = _passwordHasher.GenerateHash(newPassword);

            var userToUpdate = new UpdateUserPasswordDto
            {
                Id = userId,
                NewPassword = hashedNewPassword
            };

            await _usersRepository.UpdatePasswordAsync(userToUpdate);
            return result;
        }

        public async Task<ServiceResult> DeleteUserAsync(int userId)
        {
            var result = new ServiceResult();

            var existingUser = await _usersRepository.GetByIdAsync(userId);

            if (existingUser == null)
            {
                result.Errors.Add("User not found.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            await _usersRepository.DeleteAsync(userId);

            return result;
        }

        public async Task<ServiceResult<string>> LoginAsync(string email, string password)
        {
            var existingUser = await _usersRepository.GetByEmailAsync(email);

            if (existingUser == null)
            {
                return ServiceResult<string>.Failure("User not found.");
            }

            var verify = _passwordHasher.VerifyHash(password, existingUser.Password);

            if (verify == false)
            {
                return ServiceResult<string>.Failure("Invalid password.");
            }

            var user = new UserDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email
            };

            var token = _jwtProvider.GenerateToken(user);

            return ServiceResult<string>.Success(token);
        }
    }
}
