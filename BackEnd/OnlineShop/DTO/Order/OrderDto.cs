using OnlineShop.Entities;

namespace OnlineShop.DTO.Order
{
    public class OrderDto
    {
        public string UserName { get; set; } = string.Empty;

        public int OrderId { get; set; }

        public StatusEnum OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }

    public class OrderShortDto
    {
        public int OrderId { get; set; }

        public StatusEnum OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }
}
