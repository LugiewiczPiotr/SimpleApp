using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Categories
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
            CategoryRepositoryMock.Verify(
                x => x.Delete(It.IsAny<Category>()), Times.Never());

            ProductRepositoryMock.Verify(
               x => x.DeleteByCategoryId(It.IsAny<Guid>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Success_When_Category_Is_Deleted()
        {
            // Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            category.Id = product.CategoryId;
            CategoryRepositoryMock.Setup(
                x => x.Delete(category));
            ProductRepositoryMock.Setup(
                r => r.DeleteByCategoryId(category.Id));

            // Act
            var result = logic.Delete(category);

            // Assert
            result.Success.Should().BeTrue();
            ProductRepositoryMock.Verify(
               x => x.DeleteByCategoryId(category.Id), Times.Once());

            CategoryRepositoryMock.Verify(
                x => x.Delete(category), Times.Once());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }
    }
}
