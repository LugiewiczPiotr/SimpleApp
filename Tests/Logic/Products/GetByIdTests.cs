using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public async Task Return_Error_When_Product_Not_Exist()
        {
            // Arrange
            var logic = Create();
            ProductRepositoryMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Product)null);
            var guid = Guid.NewGuid();

            // Act
            var result = await logic.GetById(guid);

            // Assert
            result.Should().BeFailure($"Product with ID {guid} does not exist.");
            ProductRepositoryMock.Verify(
                x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public async Task Return_Product_From_RepositoryAsync()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ProductRepositoryMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                ReturnsAsync(product);

            // Act
            var result = await logic.GetById(product.Id);

            // Assert
            result.Should().BeSuccess(product);
            ProductRepositoryMock.Verify(
                x => x.GetById(product.Id), Times.Once());
        }
    }
}
