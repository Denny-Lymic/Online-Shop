using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTO.User;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("WithOrders/{userId:int}")]
        public async Task<IActionResult> GetByIdWithOrders(int userId)
        {
            var user = await _usersService.GetUserByIdWithOrdersAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user });
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var user = await _usersService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user });
        }

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst("UserId")?.Value;

            int userIdInt;
            Int32.TryParse(userId, out userIdInt);
            var user = await _usersService.GetUserByIdAsync(userIdInt);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user });
        }

        [HttpGet]
        [Route("AllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetAllUsersAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(new { users });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            var result = await _usersService.CreateUserAsync(user);

            if (!result.isSuccess)
            {
                return BadRequest(new { result.Errors });
            }

            return Ok(new { Message = "User created successfully." });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserRequest user)
        {
            var token = await _usersService.LoginAsync(user.Email, user.Password);

            if (!token.isSuccess)
            {
                return BadRequest(new { token.Errors });
            }

            if(token.Data == null)
            {
                return BadRequest(new { Message = "Something went wrong." });
            }

            //Response.Cookies.Append("killing-cookie", token.Data);

            return Ok(new { token = token.Data });
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var result = await _usersService.UpdateUserAsync(id, userDto);

            if (!result.isSuccess)
            {
                return BadRequest(new { result.Errors });
            }

            return Ok(new { Message = "User updated successfully." });
        }

        [HttpPatch]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            var result = await _usersService.UpdateUserPasswordAsync(userId, oldPassword, newPassword);
            if (!result.isSuccess)
            {
                return BadRequest(new { result.Errors });
            }
            return Ok(new { Message = "Password changed successfully." });
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _usersService.DeleteUserAsync(userId);

            if (!result.isSuccess)
            {
                return BadRequest(new { result.Errors });
            }

            return Ok(new { Message = "User deleted successfully." });
        }
    }
}
