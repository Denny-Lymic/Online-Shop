using BackEnd.Entities;

namespace BackEnd.DTO.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public StatusEnum Status { get; set; } = StatusEnum.Pending; // Default status
    }
}
