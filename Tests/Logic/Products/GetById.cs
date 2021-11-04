using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using SimpleApp.Core;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class GetById : BaseTest
    {
        [Fact]
        public void Return_Error_When_Product_Not_Exist()
        {
            //Arrange
            var logic = Create();
            ProductRespositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns((Product)null);
            var guid = Guid.NewGuid();

            //Act
            var result = logic.GetById(guid);

            //Assert
            result.Should().BeFailure($"Product with ID {guid} does not exist.");
            ProductRespositoryMock.Verify(
                x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public void Return_Product_From_Repository()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductRespositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns(product);

            //Act
            var result = logic.GetById(product.Id);

            //Assert
            result.Should().BeSuccess(product);
            ProductRespositoryMock.Verify(
                x => x.GetById(product.Id), Times.Once());

        }
    }
}
