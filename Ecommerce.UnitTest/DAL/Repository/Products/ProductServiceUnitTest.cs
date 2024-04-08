using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Model;
using Ecommerce.Core.Services;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.UnitTest.Core.Services
{
    public class ProductServiceUnitTest
    {
        [Fact]
        public async Task ViewAllProductsServiceAsync_ReturnsAllProducts()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { ID = 1, Name = "Product 1", Description = "Description 1", CurrentPrice = 10.99m },
                new ProductModel { ID = 2, Name = "Product 2", Description = "Description 2", CurrentPrice = 19.99m }
            };

            mockRepository.Setup(repo => repo.ViewAllProductsRepositoryAsync()).ReturnsAsync(expectedProducts);

            // Act
            var result = await productService.ViewAllProductsServiceAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedProducts);

            mockRepository.Verify(repo => repo.ViewAllProductsRepositoryAsync(), Times.Once);
        }

  

    [Fact]
        public async Task UpdateProductSelectedServiceAsync_UpdatesSelectedStatus()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            var productId = 1;
            var selected = true;

            var expectedUpdatedProducts = new List<ProductModel>
            {
                new ProductModel { ID = 1, Name = "Product 1", Description = "Description 1", CurrentPrice = 10.99m, Is_Selected = true }
            };

            mockRepository.Setup(repo => repo.UpdateProductSelectedRepositoryAsync(productId, selected)).ReturnsAsync(expectedUpdatedProducts);

            // Act
            var result = await productService.UpdateProductSelectedServiceAsync(productId, selected);

            // Assert
            result.Should().BeEquivalentTo(expectedUpdatedProducts);
        }

     

        [Fact]
        public async Task DeleteProductServiceAsync_DeletesProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            int productId = 1;
            int expectedDeletedCount = 1;

            mockRepository.Setup(repo => repo.DeleteProductRepositoryAsync(productId)).ReturnsAsync(expectedDeletedCount);

            // Act
            var result = await productService.DeleteProductServiceAsync(productId);

            // Assert
            result.Should().Be(expectedDeletedCount);
            mockRepository.Verify(repo => repo.DeleteProductRepositoryAsync(productId), Times.Once);
        }

        [Fact]
        public async Task FindByIdProductServiceAsync_ReturnsProductById()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            int productId = 1;
            var expectedProduct = new ProductModel { ID = 1, Name = "Product 1", Description = "Description 1", CurrentPrice = 10.99m };

            mockRepository.Setup(repo => repo.FindByIdProductRepositoryAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await productService.FindByIdProductServiceAsync(productId);

            // Assert
            result.Should().BeEquivalentTo(expectedProduct);
            mockRepository.Verify(repo => repo.FindByIdProductRepositoryAsync(productId), Times.Once);
        }

        [Fact]
        public async Task FindByNameProductServiceAsync_ReturnsProductsByName()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            string productName = "Product 1";
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { ID = 1, Name = "Product 1", Description = "Description 1", CurrentPrice = 10.99m }
            };

            mockRepository.Setup(repo => repo.FindByNameProductRepositoryAsync(productName)).ReturnsAsync(expectedProducts);

            // Act
            var result = await productService.FindByNameProductServiceAsync(productName);

            // Assert
            result.Should().BeEquivalentTo(expectedProducts);
            mockRepository.Verify(repo => repo.FindByNameProductRepositoryAsync(productName), Times.Once);
        }

        [Fact]
        public async Task CreateProductServiceAsync_CreatesProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            var newProduct = new ProductModel { ID = 1, Name = "New Product", Description = "New Description", CurrentPrice = 15.99m };

            mockRepository.Setup(repo => repo.CreateProductRepositoryAsync(newProduct)).ReturnsAsync(1);

            // Act
            var result = await productService.CreateProductServiceAsync(newProduct);

            // Assert
            result.Should().Be(1);
            mockRepository.Verify(repo => repo.CreateProductRepositoryAsync(newProduct), Times.Once);
        }

        [Fact]
        public async Task UpdateProductServiceAsync_UpdatesProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object);

            int productId = 1;
            var updatedProduct = new ProductModel { ID = 1, Name = "Updated Product", Description = "Updated Description", CurrentPrice = 20.99m };

            mockRepository.Setup(repo => repo.UpdateProductRepositoryAsync(productId, updatedProduct)).ReturnsAsync(1);

            // Act
            var result = await productService.UpdateProductServiceAsync(productId, updatedProduct);

            // Assert
            result.Should().Be(1);
            mockRepository.Verify(repo => repo.UpdateProductRepositoryAsync(productId, updatedProduct), Times.Once);
        }

    }
}

