using System;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Categories
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public void Return_Error_When_Category_Not_Exist()
        {
            // Arrange
            var logic = Create();
            CategoryRepositoryMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns((Category)null);
            var guid = Guid.NewGuid();

            // Act
            var result = logic.GetById(guid);

            // Assert
            result.Should().BeFailure($"Category with ID {guid} does not exist.");
            CategoryRepositoryMock.Verify(
                x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public void Return_Category_From_Repository()
        {
            // Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            CategoryRepositoryMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns(category);

            // Act
            var result = logic.GetById(category.Id);

            // Assert
            result.Should().BeSuccess(category);
            CategoryRepositoryMock.Verify(
                x => x.GetById(category.Id), Times.Once());
        }
    }
}
