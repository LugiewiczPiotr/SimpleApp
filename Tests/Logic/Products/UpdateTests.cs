using System;
using System.Threading;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class UpdateTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Func<Task> result = async () => await logic.UpdateAsync(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
                x => x.ValidateAsync(It.IsAny<Product>(), CancellationToken.None), Times.Never());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Return_Failure_When_Product_Is_Not_Valid()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            const string errorMessage = "validation fail";
            ValidatorMock.SetValidationFailure(product.Name, errorMessage);

            // Act
            var result = await logic.UpdateAsync(product);

            // Assert
            result.Should().BeFailure(property: product.Name, message: errorMessage);
            ValidatorMock.Verify(
                x => x.ValidateAsync(product, CancellationToken.None), Times.Once());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Return_Success_When_Product_Is_Valid()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();

            // Act
            var result = await logic.UpdateAsync(product);

            // Assert
            result.Should().BeSuccess(product);
            ValidatorMock.Verify(
                x => x.ValidateAsync(product, CancellationToken.None), Times.Once());

            ProductRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
