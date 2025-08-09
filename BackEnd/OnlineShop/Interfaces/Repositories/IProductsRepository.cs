using BackEnd.DTO.Product;
using BackEnd.Entities;

namespace BackEnd.Interfaces.Repositories
{
    public interface IProductsRepository
    {
        Task AddAsync(CreateProductDto productDto);
        Task DeleteAsync(int id);
        Task<List<ProductEntity>> GetAllAsync();
        Task<List<ProductEntity>> GetByFilterAsync(ProductSearchFilter productFilter);
        Task<ProductEntity?> GetByIdAsync(int productId);
        Task<ProductEntity?> GetByNameAsync(string productName);
        Task<List<ProductEntity>> GetByPageAsync(int page, int pageSize, string? category = null);
        Task<List<string>> GetCategoriesAsync();
        Task<double> GetMaxPriceAsync();
        Task UpdateAsync(UpdateProductDto productDto);
    }
}