namespace OnlineShop.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public double Price { get; set; }
    }

    public class ProductWithDescriptionDto : ProductDto
    {
        public string Description { get; set; } = string.Empty;
    }

    public class ProductSearchFilter
    {
        public string? Name { get; set; } = string.Empty;

        public string? Category { get; set; } = string.Empty;

        public double? LowerPrice { get; set; }

        public double? UpperPrice { get; set; }

        public string? SortBy { get; set; } = string.Empty;

        public string? SortOrder { get; set; } = string.Empty;
    }
}
