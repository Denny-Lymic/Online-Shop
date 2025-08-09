using BackEnd.DTO.Order;

namespace BackEnd.DTO.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    public class UserWithOrdersDto : UserDto
    {
        public List<OrderShortDto> Orders { get; set; } = new List<OrderShortDto>();
    }

    public class UserWithPasswordDto : UserDto
    {
        public string Password { get; set; } = string.Empty;
    }
}