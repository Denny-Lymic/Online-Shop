using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTO.Product;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsService.GetAllProductsAsync();

            if (products == null)
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        [HttpGet]
        [Route("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productsService.GetProductByIdAsync(productId);

            if (product == null)
            {
                return NotFound($"Product with ID {productId} not found.");
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetProductsByFilter([FromQuery] ProductSearchFilter productFilter)
        {
            var products = await _productsService.GetProductsByFilterAsync(productFilter);

            return Ok(products);
        }

        [HttpGet]
        [Route("Page")]
        public async Task<IActionResult> GetProductsByPage(int pageNumber, int pageSize, string? category = null)
        {
            var products = await _productsService.GetProductsByPageAsync(pageNumber, pageSize, category);

            if (products == null || !products.Any())
            {
                return NotFound("No products found for the specified page.");
            }

            return Ok(products);
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productsService.GetProductCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("Categories not found");
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("MaxPrice")]
        public async Task<IActionResult> GetMaxPrice()
        {
            var maxPrice = await _productsService.GetMaxPrice();
            if (maxPrice == 0)
            {
                return NotFound("Max price not found");
            }
            return Ok(maxPrice);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var result = await _productsService.CreateProductAsync(productDto);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Product created successfully." });
        }

        [HttpPatch]
        [Route("Update/{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductDto productDto)
        {
            var result = await _productsService.UpdateProductAsync(productId, productDto);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Product updated successfully." });
        }

        [HttpDelete]
        [Route("Delete/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productsService.DeleteProductAsync(productId);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Product deleted successfully." });
        }
    }
}
