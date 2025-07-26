namespace BackEnd.DTO.Product
{
    public class UpdateImageRequest
    {
        public required IFormFile Image { get; set; } 

        public required int productId { get; set; }
    }
}
