using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPITask.Controllers;
using WebAPITask.Services;

namespace ProductServiceTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            // Setup
            _productService = new Mock<IProductService>();
            _controller = new ProductController(_productService.Object);
        }

        [Theory]
        [InlineData("Pr1", "This is Pr1", "Pr1Manufact")]
        [InlineData("Pr2", "This is Pr2", "Pr2Manufact")]
        public void CreateProduct_ShouldCallCreateProductMethod(string name, string description, string Manufacturer)
        {
            // Act
            _controller.CreateProduct(name, description, Manufacturer);

            // Assert
            _productService.Verify(ps => ps.CreateProduct(name, description, Manufacturer), Times.Once);
        }

        [Theory]
        [InlineData("Pr1", "This is Pr1", "Pr1Manufact")]
        [InlineData("Pr2", "This is Pr2", "Pr2Manufact")]
        public void CreateProduct_ShouldReturnOk(string name, string description, string Manufacturer)
        {
            // Act
            var result = _controller.CreateProduct(name, description, Manufacturer);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteProduct_ShouldCallDeleteProductMethod(int productId)
        {
            // Act
            _controller.DeleteProduct(productId);

            // Assert
            _productService.Verify(ps => ps.DeleteProduct(productId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteProduct_WhenIdIsGreaterThanZero_ShouldReturnOk(int productId)
        {
            // Act
            var result = _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteProduct_WhenIdIsLessThanZero_ShouldReturnConflict()
        {
            // Setup
            int productId = -1;

            // Act
            var result = _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Theory]
        [InlineData(0, "New Name 1")]
        [InlineData(1, "New Name 2")]
        public void UpdateProductName_ShouldCallDeleteProductMethod(int productId, string name)
        {
            // Act
            _controller.UpdateProductName(productId, name);

            // Assert
            _productService.Verify(ps => ps.UpdateProductName(productId, name), Times.Once);
        }

        [Theory]
        [InlineData(0, "New Name 1")]
        [InlineData(1, "New Name 2")]
        public void UpdateProductName_WhenIdIsGreaterThanZero_ShouldReturnOk(int productId, string name)
        {
            // Act
            var result = _controller.UpdateProductName(productId, name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateProductName_WhenIdIsLessThanZero_ShouldReturnConflict()
        {
            // Setup
            int productId = -1;
            string name = "New Name -1";

            // Act
            var result = _controller.UpdateProductName(productId, name);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }
    }
}