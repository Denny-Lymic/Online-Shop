using BackEnd.DTO.Product;
using Microsoft.AspNetCore.Authentication;
using OnlineShop.DTO.Product;
using OnlineShop.Entities;
using OnlineShop.Models;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class ProductsService
    {
        private readonly ProductsRepository _productsRepository;

        public ProductsService(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
    
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productsRepository.GetAllAsync();

            if (products == null)
            {
                return new List<ProductDto>();
            }

            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price
            }).ToList();

            return productsDto;
        }
    
        public async Task<ProductWithDescriptionDto?> GetProductByIdAsync(int productId)
        {
            var product = await _productsRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            var productDto = new ProductWithDescriptionDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };

            return productDto;
        }

        public async Task<List<ProductDto>> GetProductsByFilterAsync(ProductSearchFilter productFilter)
        {
            var products = await _productsRepository.GetByFilterAsync(productFilter);

            if (products == null || products.Count == 0)
            {
                return new List<ProductDto>();
            }

            var productDtos = products.Select(p => new ProductDto { 
                Id = p.Id,
                Name = p.Name, 
                Category = p.Category, 
                Price = p.Price }).ToList();

            return productDtos;
        }
            
        public async Task<List<ProductDto>> GetProductsByPageAsync(int page, int pageSize, string? category = null)
        {
            var products = await _productsRepository.GetByPageAsync(page, pageSize, category);

            if (products == null)
            {
                return new List<ProductDto>();
            }

            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price
            }).ToList();

            return productsDto;
        }

        public async Task<List<CategoryDto>> GetProductCategoriesAsync()
        {
            var categories = await _productsRepository.GetCategoriesAsync();    

            var categoriesDto = categories
                .Select((c, index) => new CategoryDto { Id = index + 1, Category = c })
                .ToList();

            return categoriesDto;
        }

        public async Task<double> GetMaxPrice()
        {
            var maxPrice = await _productsRepository.GetMaxPriceAsync();    

            return maxPrice;
        }

        public async Task<ServiceResult> CreateProductAsync(CreateProductDto productDto)
        {
            var result = new ServiceResult();

            if (productDto == null)
            {
                result.Errors.Add("Product data is required.");
                return result;
            }

            var existingProduct = await _productsRepository.GetByNameAsync(productDto.Name);

            if (existingProduct != null)
            {
                result.Errors.Add("Product with this name already exists.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                result.Errors.Add("Product name is required.");
            }
            if (string.IsNullOrWhiteSpace(productDto.Category))
            {
                result.Errors.Add("Product category is required.");
            }
            if (!productDto.Price.HasValue)
            {
                result.Errors.Add("Product price is required.");
            }
            if (string.IsNullOrWhiteSpace(productDto.Description))
            {
                result.Errors.Add("Product description is required.");
            }
            if(!result.isSuccess)
            {
                return result;
            }

            await _productsRepository.AddAsync(productDto);

            return result;
        }
    
        public async Task<ServiceResult> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var result = new ServiceResult();

            var existingProduct = await _productsRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                result.Errors.Add("Product not found.");
                return result;
            }

            if (!string.IsNullOrEmpty(productDto.Name))
            {
                if (!string.Equals(existingProduct.Name, productDto.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var existingProductByName = await _productsRepository.GetByNameAsync(productDto.Name);

                    if (existingProductByName != null 
                        && string.Equals(existingProductByName.Name, productDto.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Errors.Add("This product name is already exist");
                    }
                }
            }
            else
            {
                productDto.Name = existingProduct.Name;
            }
            if (string.IsNullOrEmpty(productDto.Category))
            {
                productDto.Category = existingProduct.Category;
            }
            if (!productDto.Price.HasValue)
            {
                productDto.Price = existingProduct.Price;
            }

            await _productsRepository.UpdateAsync(id, productDto);

            return result;
        }
    
        public async Task<ServiceResult> DeleteProductAsync(int productId)
        {
            var result = new ServiceResult();

            var existingProduct = await _productsRepository.GetByIdAsync(productId);

            if (existingProduct == null)
            {
                result.Errors.Add("Product not found.");
                return result;
            }

            await _productsRepository.DeleteAsync(productId);

            return result;
        }
    }
}
