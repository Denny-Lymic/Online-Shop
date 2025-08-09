namespace BackEnd.DTO.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Category { get; set; } = string.Empty;

        public double? Price { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;
    }
}
