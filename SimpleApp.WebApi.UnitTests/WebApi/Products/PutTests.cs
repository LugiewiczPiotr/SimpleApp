using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Products
{
    public class PutTests : BaseTests
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
            ProductLogicMock
                .Verify(x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public void Return_BadRequest_When_Product_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var productDto = Builder<ProductDto>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            var errorMessage = "BadRequest";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                 Returns(Result.Ok(product));
            MapperMock
                .Setup(x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()));
            ProductLogicMock
                .Setup(r => r.Update(product))
                .Returns(Result.Failure<Product>(product.Name, errorMessage));

            //Act
            var result = logic.Put(productDto.Id, productDto);

            //Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetById(productDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(product), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()));
        }

        [Fact]
        public void Return_Created_Category_When_Product_is_Valid()
        {
            //Arrange
            var logic = Create();
            var productDto = Builder<ProductDto>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(product));
            MapperMock
                .Setup(x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()));
            ProductLogicMock
                .Setup(r => r.Update(product))
                .Returns(Result.Ok(product));
            MapperMock
                .Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(productDto);

            //Act
            var result = logic.Put(productDto.Id, productDto);

            //Assert
            result.Should().BeOk(productDto);
            ProductLogicMock.Verify(
                x => x.GetById(productDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(product), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Once());
        }
    }
}
