using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Products
{
    public class Delete : BaseTest
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
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Return_BeBadRequest_When_Product_Is_Not_Valid()
        {
            //Arrange
            var errorMessage = "BadRequest";
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(product));
            ProductLogicMock
                .Setup(r => r.Delete(It.IsAny<Product>()))
                .Returns(Result.Failure<Category>(product.Name, errorMessage));

            //Act
            var result = logic.Delete(product.Id);

            //Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(product), Times.Once());
        }

        [Fact]
        public void Return_NoContent_When_Product_Is_Deleted()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(product));
            ProductLogicMock
                .Setup(r => r.Delete(product)).Returns(Result.Ok());


            //Act
            var result = logic.Delete(product.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
            ProductLogicMock
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(product), Times.Once());
        }
    }
}
