using BackEnd.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.DTO.Product;
using OnlineShop.Entities;
using System.Linq.Expressions;

namespace OnlineShop.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ShopDbContext _context;
        public ProductsRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductEntity?> GetByIdAsync(int productId)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<ProductEntity?> GetByNameAsync(string productName)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == productName);
        }

        public async Task<List<ProductEntity>> GetByFilterAsync(ProductSearchFilter productFilter)
        {
            Expression<Func<ProductEntity, object>> selectorKey = productFilter.SortBy?.ToLower() switch
            {
                "price" => product => product.Price,
                "name" => product => product.Name,
                _ => product => product.Id,
            };

            var query = _context.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(productFilter.Search))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{productFilter.Search}%"));
            }

            if (productFilter.MinPrice > 0)
            {
                query = query.Where(p => p.Price >= productFilter.MinPrice);
            }

            if (productFilter.MaxPrice < 30000)
            {
                query = query.Where(p => p.Price <= productFilter.MaxPrice);
            }

            if (!string.IsNullOrEmpty(productFilter.Category))
            {
                query = query.Where(p => EF.Functions.Like(p.Category, $"%{productFilter.Category}%"));
            }

            query = productFilter.SortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(selectorKey)
                : query.OrderBy(selectorKey);

            return await query.ToListAsync();
        }

        public async Task<List<ProductEntity>> GetByPageAsync(int page, int pageSize, string? category = null)
        {

            var query = _context.Products.AsNoTracking();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => EF.Functions.Like(p.Category, $"{category}"));
            }

            query = query.Skip((page - 1) * pageSize)
                        .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<double> GetMaxPriceAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .MaxAsync(p => p.Price);
        }

        public async Task AddAsync(CreateProductDto productDto)
        {
            var product = new ProductEntity
            {
                Name = productDto.Name,
                Category = productDto.Category,
                Price = productDto.Price ?? 0,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl ?? "",
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateProductDto productDto)
        {
            await _context.Products
                .Where(p => p.Id == productDto.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(x => x.Name, productDto.Name)
                    .SetProperty(x => x.Category, productDto.Category)
                    .SetProperty(x => x.Price, productDto.Price)
                    .SetProperty(x => x.Description, productDto.Description)
                    .SetProperty(x => x.ImageUrl, productDto.ImageUrl));
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
