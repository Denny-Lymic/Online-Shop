using BackEnd.Interfaces.Repositories;
using BackEnd.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineShop.DTO.Product;
using OnlineShop.Entities;
using OnlineShop.Services;

namespace xUnitTests.Core
{
    public class ProductsServiceTests : IClassFixture<FakeWebHostEnvironment>
    {
        private readonly Mock<IProductsRepository> _repoMock = new();
        private readonly IProductsService _sut;
        private readonly FakeWebHostEnvironment _fe;

        public ProductsServiceTests(FakeWebHostEnvironment fe)
        {
            _fe = fe;

            var sp = new ServiceCollection()
                .AddSingleton(fe.Env)
                .AddSingleton(_repoMock.Object)
                .AddTransient<IProductsService, ProductsService>()
                .BuildServiceProvider();

            _sut = sp.GetRequiredService<IProductsService>();
        }

        [Fact]
        public async Task ServiceAddAsync_CorrectDto_AddsProducts()
        {
            // arange

            _repoMock.Setup(r => r.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((ProductEntity ?)null);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<CreateProductDto>()))
                .Returns(Task.CompletedTask);

            var productDto = new CreateProductDto
            {
                Name = "Ryzen 5600",
                Category = "CPU",
                Price = 5000,
                ImageUrl = "5aa7ad6b-0cdb-4dda-bd80-52ce9e538145.png",
                Description = "Description",
                Image = ImageStub.GetFormFile()
            };

            // act

            var result = await _sut.CreateProductAsync(productDto);

            // assert

            Assert.True(result.isSuccess);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<CreateProductDto>()), Times.Once());

            var saved = Path.Combine(_fe.Env.WebRootPath, "images", "products", productDto.ImageUrl!);
            Assert.True(Path.Exists(saved));

            (productDto.Image.OpenReadStream() as MemoryStream)?.Dispose();
        }

        [Fact]
        public async Task ServiceAddAsync_ProductAlreadyExists_AddsProducts()
        {
            // arange

            _repoMock.Setup(r => r.GetByNameAsync("Ryzen 5600"))
                .ReturnsAsync(new ProductEntity{ Name = "Ryzen 5600" });

            var productDto = new CreateProductDto
            {
                Name = "Ryzen 5600",
                Category = "CPU",
                Price = 5000,
                ImageUrl = "5aa7ad6b-0cdb-4dda-bd80-52ce9e538145.png",
                Description = "Description",
                Image = ImageStub.GetFormFile()
            };

            // act

            var result = await _sut.CreateProductAsync(productDto);

            // assert

            Assert.False(result.isSuccess);
            Assert.Contains("Product with this name already exists.", result.Errors.Single());

            _repoMock.Verify(r => r.AddAsync(It.IsAny<CreateProductDto>()), Times.Never);

            (productDto.Image.OpenReadStream() as MemoryStream)?.Dispose();
        }

        [Fact]
        public async Task ServiceAddAsync_IncorrectProduct_AddsProducts()
        {
            // arange

            _repoMock.Setup(r => r.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((ProductEntity ?)null);

            var productDto = new CreateProductDto
            {
                Image = ImageStub.GetFormFile()
            };

            // act

            var result = await _sut.CreateProductAsync(productDto);

            // assert

            Assert.False(result.isSuccess);
            Assert.Contains("Product name is required.", result.Errors);
            Assert.Contains("Product category is required.", result.Errors);
            Assert.Contains("Product price is required.", result.Errors);
            Assert.Contains("Product description is required.", result.Errors);

            _repoMock.Verify(r => r.AddAsync(It.IsAny<CreateProductDto>()), Times.Never);

            (productDto.Image.OpenReadStream() as MemoryStream)?.Dispose();
        }
    }
}
