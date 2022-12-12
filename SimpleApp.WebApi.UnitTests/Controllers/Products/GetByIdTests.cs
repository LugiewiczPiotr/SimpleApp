using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public async Task Return_NotFound_When_Product_Not_Exist()
        {
            // Arrange
            var controller = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Failure<Product>(errorMessage));

            // Act
            var result = await controller.Get(guid);

            // Assert
            result.Should().BeNotFound<Product>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetById(guid), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public async Task Return_Ok_When_Product_is_Exist()
        {
            // Arrange
            var controller = Create();
            var product = Builder<Product>.CreateNew().Build();
            var productDto = Builder<ProductDto>.CreateNew().Build();
            ProductLogicMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Ok(product));
            MapperMock.Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(productDto);

            // Act
            var result = await controller.Get(product.Id);

            // Assert
            result.Should().BeOk(productDto);
            ProductLogicMock.Verify(
                x => x.GetById(product.Id), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(product), Times.Once());
        }
    }
}
