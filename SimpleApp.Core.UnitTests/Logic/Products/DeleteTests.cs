using System;
using System.Threading.Tasks;
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
            Func<Task> result = async () => await logic.DeleteAsync(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            ProductRepositoryMock.Verify(
                x => x.Delete(It.IsAny<Product>()), Times.Never());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Return_Success_When_Product_Is_DeletedAsync()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductRepositoryMock
                .Setup(r => r.Delete(product));

            // Act
            var result = await logic.DeleteAsync(product);

            // Assert
            result.Success.Should().BeTrue();
            ProductRepositoryMock.Verify(
                x => x.Delete(product), Times.Once());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
