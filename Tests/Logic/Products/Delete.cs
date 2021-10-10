using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;

namespace Tests.Logic.Products
{
    public class Delete : BaseTest
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            //Arrange
            var logic = Create();

            //Act
            Action result = () => logic.Delete(null);

            //Assert
            result.Should().Throw<ArgumentNullException>();
            ProductRespositoryMock.Verify(
                x => x.Delete(null), Times.Never());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Succes_When_Product_Is_Deleted()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductRespositoryMock.Setup(r => r.Delete(product));

            //Act
            var result = logic.Delete(product);

            //Assert
            result.Success.Should().BeTrue();
            ProductRespositoryMock.Verify(
                x => x.Delete(product), Times.Once());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }


    }
}
