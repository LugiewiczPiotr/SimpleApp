using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public async Task Return_NotFound_When_Category_Not_Exist()
        {
            // Arrange
            var controller = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Category with ID {guid} does not exist.";
            CategoryLogicMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Failure<Category>(errorMessage));

            // Act
            var result = await controller.GetAsync(guid);

            // Assert
            result.Should().BeNotFound<Category>(errorMessage);
            CategoryLogicMock.Verify(
                x => x.GetByIdAsync(guid), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public async Task Return_Ok_Category_When_Category_is_Exist()
        {
            // Arrange
            var controller = Create();
            var category = Builder<Category>.CreateNew().Build();
            var categoryDto = Builder<CategoryDto>.CreateNew().Build();
            CategoryLogicMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Ok(category));
            MapperMock.Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(categoryDto);

            // Act
            var result = await controller.GetAsync(category.Id);

            // Assert
            result.Should().BeOk(categoryDto);
            CategoryLogicMock.Verify(
                x => x.GetByIdAsync(category.Id), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(category), Times.Once());
        }
    }
}
