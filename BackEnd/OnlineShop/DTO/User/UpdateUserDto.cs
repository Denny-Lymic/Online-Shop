namespace OnlineShop.DTO.User
{
    public class UpdateUserDto
    {
        public string? Name { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
    }

    public class UpdateUserPasswordDto
    {
        public int Id { get; set; }

        public string NewPassword { get; set; } = string.Empty;
    }
}
