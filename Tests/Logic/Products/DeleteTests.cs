using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class DeleteTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Action result = () => logic.Delete(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            ProductRespositoryMock.Verify(
                x => x.Delete(It.IsAny<Product>()), Times.Never());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Succes_When_Product_Is_Deleted()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductRespositoryMock
                .Setup(r => r.Delete(product));

            // Act
            var result = logic.Delete(product);

            // Assert
            result.Success.Should().BeTrue();
            ProductRespositoryMock.Verify(
                x => x.Delete(product), Times.Once());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }
    }
}
