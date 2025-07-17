namespace OnlineShop.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserEntity User { get; set; } = null!;

        public int ProductId { get; set; }

        public ProductEntity? Product { get; set; }

        public StatusEnum Status { get; set; } = StatusEnum.Pending;

        public DateTime OrderDate { get; set; }
    }
}
