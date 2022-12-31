using System;
using System.Threading;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Categories
{
    public class AddTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Func<Task> result = async () => await logic.AddAsync(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
               x => x.ValidateAsync(It.IsAny<Category>(), CancellationToken.None), Times.Never());

            CategoryRepositoryMock.Verify(
               x => x.AddAsync(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Return_Failure_When_Category_Is_Not_Valid()
        {
            // Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            const string errorMessage = "validation fail";
            ValidatorMock.SetValidationFailure(category.Name, errorMessage);

            // Act
            var result = await logic.AddAsync(category);

            // Assert
            result.Should().BeFailure(property: category.Name, message: errorMessage);
            ValidatorMock.Verify(
                x => x.ValidateAsync(category, CancellationToken.None), Times.Once());

            CategoryRepositoryMock.Verify(
               x => x.AddAsync(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Return_Success_When_Category_Is_Valid()
        {
            // Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();

            // Act
            var result = await logic.AddAsync(category);

            // Assert
            result.Should().BeSuccess(category);
            ValidatorMock.Verify(
               x => x.ValidateAsync(category, CancellationToken.None), Times.Once);

            CategoryRepositoryMock.Verify(
               x => x.AddAsync(category), Times.Once());

            CategoryRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
