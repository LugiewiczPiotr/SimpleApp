using Microsoft.AspNetCore.Mvc;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class PostTests : BaseTests
    {
        private Product product;
        private ProductDto productDto;

        private void CorrectFlow()
        {
            product = Builder<Product>.CreateNew().Build();
            productDto = Builder<ProductDto>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Product>(It.IsAny<ProductDto>()))
                .Returns(product);
            ProductLogicMock.Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(Result.Ok(product));
            MapperMock.Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(productDto);
        }

        protected override ProductController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        [Fact]
        public void Return_BeBadRequest_When_Product_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "validation fail";
            ProductLogicMock
                .Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(Result.Failure<Product>(product.Name, errorMessage));

            // Act
            var result = controller.Post(productDto);

            // Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            MapperMock.Verify(
                x => x.Map<Product>(productDto), Times.Once());

            ProductLogicMock.Verify(
               x => x.Add(product), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Product_Is_Valid()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = controller.Post(productDto);

            // Assert
            result.Should().BeCreatedAtAction(productDto);
            MapperMock.Verify(
              x => x.Map<Product>(productDto), Times.Once());

            ProductLogicMock.Verify(
               x => x.Add(product), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(product), Times.Once());
        }
    }
}
