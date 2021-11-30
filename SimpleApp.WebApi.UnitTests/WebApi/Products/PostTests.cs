using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Products
{
    public class PostTests : BaseTests
    {
        [Fact]
        public void Return_BeBadRequest_When_Product_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var productDto = Builder<ProductDto>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            var errorMessage = "validation fail";
            MapperMock
                .Setup(x => x.Map<Product>(productDto))
                .Returns(product);
            ProductLogicMock
                .Setup(x => x.Add(product))
                .Returns(Result.Failure<Product>(product.Name, errorMessage));

            //Act
            var result = logic.Post(productDto);

            //Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            ProductLogicMock.Verify(
               x => x.Add(product), Times.Once());

            MapperMock.Verify(
                x => x.Map<Product>(productDto), Times.Once());

            MapperMock.Verify(
               x => x.Map<ProductDto>(product), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Product_Is_Valid()
        {
            //Arrange
            var logic = Create();
            var productDto = Builder<ProductDto>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Product>(productDto))
                .Returns(product);
            ProductLogicMock.Setup(x => x.Add(product))
                .Returns(Result.Ok(product));
            MapperMock.Setup(x => x.Map<ProductDto>(product))
                .Returns(productDto);

            //Act
            var result = logic.Post(productDto);

            //Assert
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
