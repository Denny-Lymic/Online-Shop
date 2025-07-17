namespace OnlineShop.DTO.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public double? Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
