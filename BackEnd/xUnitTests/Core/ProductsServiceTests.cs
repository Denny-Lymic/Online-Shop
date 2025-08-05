using BackEnd.Interfaces.Repositories;
using BackEnd.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineShop.DTO.Product;
using OnlineShop.Entities;
using OnlineShop.Services;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task ServiceAddProductAsync_CorrectDto_AddsProducts()
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
        public async Task ServiceAddProductAsync_ProductAlreadyExists_ShouldBeError()
        {
            // arange

            _repoMock.Setup(r => r.GetByNameAsync("Ryzen 5600"))
                .ReturnsAsync(new ProductEntity{ Name = "Ryzen 5600" });

            var productDto = new CreateProductDto
            {
                Name = "Ryzen 5600",
                Category = "CPU",
                Price = 5000,
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
        public async Task ServiceAddProductAsync_IncorrectProduct_ShouldBeError()
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

        [Fact]
        public async Task ServiceUpdateProductAsync_CorrectDto_UpdatesProduct()
        {
            // arange

            var productDto = new UpdateProductDto
            {
                Name = "Ryzen 5600",
                Category = "CPU",
                Price = 5000,
                Description = "Description",
                Image = ImageStub.GetFormFile()
            };

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductEntity
                {
                    Name = "Ryzen 5600",
                    Category = "CPU",
                    Price = 5000,
                });
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<UpdateProductDto>()))
                .Returns(Task.CompletedTask);
            
            // act

            var result = await _sut.UpdateProductAsync(productDto);

            // assert

            Assert.True(result.isSuccess);

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<UpdateProductDto>()), Times.Once);
            _repoMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);

            var saved = Path.Combine(_fe.Env.WebRootPath, "images", "products", productDto.ImageUrl!);
            Assert.True(Path.Exists(saved));

            (productDto.Image.OpenReadStream() as MemoryStream)?.Dispose();
        }

        [Fact]
        public async Task ServiceUpdateProductAsync_EmptyDto_ShouldBeError()
        {
            // arange

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ProductEntity ?) null);
            
            // act

            var result = await _sut.UpdateProductAsync(new UpdateProductDto());

            // assert

            Assert.False(result.isSuccess);
            Assert.Contains("Product not found.", result.Errors);

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<UpdateProductDto>()), Times.Never);
        }

        [Fact]
        public async Task ServiceUpdateProductAsync_SameNameAsExistingProduct_ShouldBeError()
        {
            // arange

            var product = new UpdateProductDto
            {
                Name = "Ryzen 7600X",
                Category = "CPU",
                Price = 5000,
                Description = "Description",
                Image = ImageStub.GetFormFile()
            };

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductEntity
                {
                    Name = "Ryzen 5600",
                    Category = "CPU",
                    Price = 5000,
                });

            _repoMock.Setup(r => r.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ProductEntity 
                {
                    Name = "Ryzen 7600X",
                    Category = "CPU",
                    Price = 10000,
                });

            // act

            var result = await _sut.UpdateProductAsync(product);

            // assert

            Assert.False(result.isSuccess);
            Assert.Contains("This product name is already exist", result.Errors);

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<UpdateProductDto>()), Times.Never);
        }

        [Fact]
        public async Task ServiceDeleteProductAsync_CorrectId_DeletingProduct() {
            // arange

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductEntity
                {
                    Name = "Ryzen",
                });
            _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // act

            var result = await _sut.DeleteProductAsync(It.IsAny<int>());

            // assert

            Assert.True(result.isSuccess);
            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task ServiceDeleteProductAsync_IncorrectId_ShouldBeError()
        {
            // arange

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ProductEntity ?) null);

            // act

            var result = await _sut.DeleteProductAsync(It.IsAny<int>());

            // assert

            Assert.False(result.isSuccess);
            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never());
        }
    }
}
