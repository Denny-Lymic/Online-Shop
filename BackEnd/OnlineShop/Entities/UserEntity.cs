namespace BackEnd.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        
        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();  
    }
}
