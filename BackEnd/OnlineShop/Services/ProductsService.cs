using BackEnd.DTO.Product;
using BackEnd.Interfaces.Repositories;
using BackEnd.Interfaces.Services;
using OnlineShop.DTO.Product;
using OnlineShop.Models;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsService(IProductsRepository productsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productsRepository = productsRepository;
            _webHostEnvironment = webHostEnvironment;
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
                Price = p.Price,
                ImageUrl = p.ImageUrl
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
                ImageUrl = product.ImageUrl,
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

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
            }).ToList();

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
                Price = p.Price,
                ImageUrl = p.ImageUrl,
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

            if (productDto.Image == null || productDto.Image.Length == 0)
            {
                result.Errors.Add("Image file is required.");
                return result;
            }

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
            if (!result.isSuccess)
            {
                return result;
            }

            var extension = Path.GetExtension(productDto.Image.FileName);
            var newName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", newName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await productDto.Image.CopyToAsync(stream);

            productDto.ImageUrl = newName;
            await _productsRepository.AddAsync(productDto);

            return result;
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductDto productDto)
        {
            var result = new ServiceResult();

            var existingProduct = await _productsRepository.GetByIdAsync(productDto.Id);

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
                productDto.Name = existingProduct.Name;

            if (string.IsNullOrEmpty(productDto.Category))
                productDto.Category = existingProduct.Category;

            if (!productDto.Price.HasValue)
                productDto.Price = existingProduct.Price;

            if (productDto.Image == null || productDto.Image.Length == 0)
            {
                productDto.ImageUrl = existingProduct.ImageUrl;
            }
            else
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", existingProduct.ImageUrl);
                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);

                var extension = Path.GetExtension(productDto.Image.FileName);
                var newName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", newName);
                productDto.ImageUrl = newName;

                await using var stream = new FileStream(filePath, FileMode.Create);
                await productDto.Image.CopyToAsync(stream);
            }

            await _productsRepository.UpdateAsync(productDto);

            return result;
        }

        public async Task<ServiceResult> UpdateImageAsync(UpdateImageRequest imageRequest)
        {
            var result = new ServiceResult();
            try
            {
                if (imageRequest.Image == null || imageRequest.Image.Length == 0)
                {
                    throw new Exception("Image file is required.");
                }

                var product = await _productsRepository.GetByIdAsync(imageRequest.productId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                var extension = Path.GetExtension(imageRequest.Image.FileName);
                var newName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", newName);

                await UpdateProductAsync(new UpdateProductDto { Id = imageRequest.productId, ImageUrl = newName });

                await using var stream = new FileStream(filePath, FileMode.Create);
                await imageRequest.Image.CopyToAsync(stream);

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);

                return result;
            }
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

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products", existingProduct.ImageUrl);
            if (File.Exists(oldImagePath))
                File.Delete(oldImagePath);

            await _productsRepository.DeleteAsync(productId);

            return result;
        }
    }
}
