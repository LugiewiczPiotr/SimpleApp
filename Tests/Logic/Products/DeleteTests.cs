using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models.Entities;
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
            ProductRepositoryMock.Verify(
                x => x.Delete(It.IsAny<Product>()), Times.Never());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public void Return_Success_When_Product_Is_Deleted()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductRepositoryMock
                .Setup(r => r.Delete(product));

            // Act
            var result = logic.Delete(product);

            // Assert
            result.Success.Should().BeTrue();
            ProductRepositoryMock.Verify(
                x => x.Delete(product), Times.Once());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
