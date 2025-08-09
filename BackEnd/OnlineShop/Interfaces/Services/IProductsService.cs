using BackEnd.DTO.Product;
using BackEnd.Models;

namespace BackEnd.Interfaces.Services
{
    public interface IProductsService
    {
        Task<ServiceResult> CreateProductAsync(CreateProductDto productDto);
        Task<ServiceResult> DeleteProductAsync(int productId);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<double> GetMaxPrice();
        Task<ProductWithDescriptionDto?> GetProductByIdAsync(int productId);
        Task<List<CategoryDto>> GetProductCategoriesAsync();
        Task<List<ProductDto>> GetProductsByFilterAsync(ProductSearchFilter productFilter);
        Task<List<ProductDto>> GetProductsByPageAsync(int page, int pageSize, string? category = null);
        Task<ServiceResult> UpdateImageAsync(UpdateImageRequest imageRequest);
        Task<ServiceResult> UpdateProductAsync(UpdateProductDto productDto);
    }
}