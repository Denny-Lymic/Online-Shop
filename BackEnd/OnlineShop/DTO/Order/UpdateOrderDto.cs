using OnlineShop.Entities;

namespace OnlineShop.DTO.Order
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public StatusEnum Status { get; set; }
    }
}
