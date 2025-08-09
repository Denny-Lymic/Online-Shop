using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO.User
{
    public class CreateUserDto
    {
        [Required] public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required] public string Password { get; set; } = string.Empty;

        [Required] public string Email { get; set; } = string.Empty;
    }
}
