using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Products
{
    public class Get_Id : BaseTest
    {
        [Fact]
        public void Return_NotFound_When_Product_Not_Exist()
        {
            //Arrange
            var logic = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Product>(errorMessage));

            //Act
            var result = logic.Get(guid);

            //Assert
            result.Should().BeNotFound<Product>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetById(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Return_Ok_When_Product_is_Exist()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            var productDto = Builder<ProductDto>.CreateNew().Build();
            ProductLogicMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(product));
            MapperMock.Setup(x => x.Map<ProductDto>(product))
                .Returns(productDto);

            //Act
            var result = logic.Get(product.Id);

            //Assert
            result.Should().BeOk(productDto);
            ProductLogicMock.Verify(
                x => x.GetById(product.Id), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(product), Times.Once());
        }
    }
}
