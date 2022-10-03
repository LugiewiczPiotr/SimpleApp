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
        private Product Product;
        private ProductDto ProductDto;

        private void CorrectFlow()
        {
            Product = Builder<Product>.CreateNew().Build();
            ProductDto = Builder<ProductDto>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Product>(It.IsAny<ProductDto>()))
                .Returns(Product);
            ProductLogicMock.Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(Result.Ok(Product));
            MapperMock.Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(ProductDto);
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
                .Returns(Result.Failure<Product>(Product.Name, errorMessage));

            // Act
            var result = controller.Post(ProductDto);

            // Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            MapperMock.Verify(
                x => x.Map<Product>(ProductDto), Times.Once());

            ProductLogicMock.Verify(
               x => x.Add(Product), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Product_Is_Valid()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = controller.Post(ProductDto);

            // Assert
            result.Should().BeCreatedAtAction(ProductDto);
            MapperMock.Verify(
              x => x.Map<Product>(ProductDto), Times.Once());

            ProductLogicMock.Verify(
               x => x.Add(Product), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(Product), Times.Once());
        }
    }
}
