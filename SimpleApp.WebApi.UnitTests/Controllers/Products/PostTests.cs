using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class PostTests : BaseTests
    {
        private Product _product;
        private ProductDto _productDto;

        [Fact]
        public void Return_BeBadRequest_When_Product_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "validation fail";
            ProductLogicMock
                .Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(Result.Failure<Product>(_product.Name, errorMessage));

            // Act
            var result = controller.PostAsync(_productDto);

            // Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            MapperMock.Verify(
                x => x.Map<Product>(_productDto), Times.Once());

            ProductLogicMock.Verify(
               x => x.Add(_product), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Product_Is_Valid()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = controller.PostAsync(_productDto);

            // Assert
            result.Should().BeCreatedAtAction(_productDto);
            MapperMock.Verify(
              x => x.Map<Product>(_productDto), Times.Once());

            ProductLogicMock.Verify(
               x => x.Add(_product), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(_product), Times.Once());
        }

        protected override ProductController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        private void CorrectFlow()
        {
            _product = Builder<Product>.CreateNew().Build();
            _productDto = Builder<ProductDto>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Product>(It.IsAny<ProductDto>()))
                .Returns(_product);
            ProductLogicMock.Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(Result.Ok(_product));
            MapperMock.Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(_productDto);
        }
    }
}
