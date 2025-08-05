using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTO.Product;
using OnlineShop.Entities;
using OnlineShop.Repositories;

namespace xUnitTests.Data
{
    public class ProductsRepoTests : InMemoryDb
    {
        private List<ProductEntity> products =
        [
            new ProductEntity
            {
                Name = "Ryzen 5600",
                Category = "CPU",
                Price = 5000,
                ImageUrl = "5aa7ad6b-0cdb-4dda-bd80-52ce9e538145.png",
                Description = "",

            },
            new ProductEntity
            {
                Name = "Ryzen 5600X",
                Category = "CPU",
                Price = 10000,
                ImageUrl = "8b7f5424-9630-4691-b3ca-e57912eecc5b.jpg",
                Description = "",

            },
            new ProductEntity
            {
                Name = "Nvidia RTX 3060",
                Category = "GPU",
                Price = 8000,
                ImageUrl = "8b7f5424-9630-4691-b3ca-e57912eecc5b.jpg",
                Description = "",

            },
        ];

        [Fact]
        public async Task GetAllProducts_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var productList = await repo.GetAllAsync();

            // assert

            productList.Should().HaveCount(3);
            productList[1].Name.Should().Be("Ryzen 5600X");
        }

        [Fact]
        public async Task GetByIdAsync_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var productList = await repo.GetByIdAsync(2);

            // assert

            productList.Should().NotBeNull();
            productList.Name.Should().Be("Ryzen 5600X");
        }

        [Fact]
        public async Task GetByNameAsync_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var productList = await repo.GetByNameAsync("Ryzen 5600X");

            // assert

            productList.Should().NotBeNull();
            productList.Name.Should().Be("Ryzen 5600X");
        }

        [Fact]
        public async Task GetByFilterAsync_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            ProductSearchFilter filter = new ProductSearchFilter()
            {
                Search = "Ryzen",
                Category = "CPU",
                MinPrice = 6000,
                SortBy = "price",
                SortOrder = "desc",
            };

            // act

            var productList = await repo.GetByFilterAsync(filter);

            // assert

            productList.Should().HaveCount(1);
            productList[0].Name.Should().Be("Ryzen 5600X");
        }

        [Fact]
        public async Task GetByPageAsync_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var productList = await repo.GetByPageAsync(0, 3);

            // assert

            productList.Should().HaveCount(3);
            productList[1].Name.Should().Be("Ryzen 5600X");
        }

        [Fact]
        public async Task GetCategoriesAsync_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var categories = await repo.GetCategoriesAsync();

            // assert

            categories.Should().HaveCount(2);
            categories.Should().Contain("GPU");
        }

        [Fact]
        public async Task GetMaxPriceAsync_Get_ReturnsData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var maxPrice = await repo.GetMaxPriceAsync();

            // assert

            maxPrice.Should().Be(10000);
        }

        [Fact]
        public async Task AddAsync_Add_AddsProducts()
        {
            // arrange

            await using var testCtx = CreateContext();
            var repo = new ProductsRepository(testCtx);

            // act

            foreach (var product in products)
            {
                await testCtx.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            // assert

            var productList = await testCtx.Products.ToListAsync();

            foreach (var product in products)
            {
                productList.Should().Contain(p => p.Name == product.Name);  
            }
        }

        [Fact]
        public async Task UpdateAsync_Update_UpdateData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var p in products)
            {
                await testCtx.Products.AddAsync(p);
            }
            await testCtx.SaveChangesAsync();
            testCtx.ChangeTracker.Clear();

            var repo = new ProductsRepository(testCtx);

            var existingProduct = await testCtx.Products
                .AsNoTracking()
                .FirstAsync(p => p.Name == "Ryzen 5600X");

            UpdateProductDto productDto = new UpdateProductDto()
            {
                Id = existingProduct.Id,
                Name = "Ryzen 7600",
                Price = existingProduct.Price,
                Category = existingProduct.Category,
                Description = existingProduct.Description,
                ImageUrl = existingProduct.ImageUrl
            };

            // act

            await repo.UpdateAsync(productDto);

            // assert

            var product = await testCtx.Products.FirstAsync(p => p.Id == existingProduct.Id);

            product.Should().NotBeNull();
            product.Name.Should().Be("Ryzen 7600");
        }

        [Fact]
        public async Task DeleteAsync_Delete_DeleteData()
        {
            // arrange

            await using var testCtx = CreateContext();

            foreach (var product in products)
            {
                await testCtx.Products.AddAsync(product);
            }
            await testCtx.SaveChangesAsync();

            var repo = new ProductsRepository(testCtx);

            // act

            var existingProduct = await testCtx.Products
                .AsNoTracking()
                .FirstAsync(p => p.Name == "Ryzen 5600X");
            await repo.DeleteAsync(existingProduct.Id);

            // assert

            var deletedProduct = await testCtx.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == existingProduct.Id);

            deletedProduct.Should().BeNull();
            
        }
    }
}
