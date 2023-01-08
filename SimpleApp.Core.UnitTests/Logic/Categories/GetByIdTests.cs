using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Categories
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public async Task Return_Error_When_Category_Not_ExistAsync()
        {
            // Arrange
            var logic = Create();
            CategoryRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).
                ReturnsAsync((Category)null);
            var guid = Guid.NewGuid();

            // Act
            var result = await logic.GetByIdAsync(guid);

            // Assert
            result.Should().BeFailure($"Category with ID {guid} does not exist.");
            CategoryRepositoryMock.Verify(
                x => x.GetByIdAsync(guid), Times.Once());
        }

        [Fact]
        public async Task Return_Category_From_RepositoryAsync()
        {
            // Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            CategoryRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).
                ReturnsAsync(category);

            // Act
            var result = await logic.GetByIdAsync(category.Id);

            // Assert
            result.Should().BeSuccess(category);
            CategoryRepositoryMock.Verify(
                x => x.GetByIdAsync(category.Id), Times.Once());
        }
    }
}
