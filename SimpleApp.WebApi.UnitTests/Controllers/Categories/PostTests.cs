using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entity;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class PostTests : BaseTests
    {
        private Category _category;
        private CategoryDto _categoryDto;
        [Fact]
        public void Return_BeBadRequest_When_Category_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "validation fail";
            CategoryLogicMock
                .Setup(x => x.Add(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(_category.Name, errorMessage));

            // Act
            var result = controller.Post(_categoryDto);

            // Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            MapperMock.Verify(
               x => x.Map<Category>(_categoryDto), Times.Once());

            CategoryLogicMock.Verify(
               x => x.Add(_category), Times.Once());

            MapperMock.Verify(
              x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Category_Is_Valid()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = controller.Post(_categoryDto);

            // Assert
            result.Should().BeCreatedAtAction(_categoryDto);
            MapperMock.Verify(
               x => x.Map<Category>(_categoryDto), Times.Once());

            CategoryLogicMock.Verify(
               x => x.Add(_category), Times.Once());

            MapperMock.Verify(
               x => x.Map<CategoryDto>(_category), Times.Once());
        }

        protected override CategoryController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        private void CorrectFlow()
        {
            _category = Builder<Category>.CreateNew().Build();
            _categoryDto = Builder<CategoryDto>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Category>(It.IsAny<CategoryDto>()))
               .Returns(_category);
            CategoryLogicMock.Setup(x => x.Add(It.IsAny<Category>()))
                .Returns(Result.Ok(_category));
            MapperMock.Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(_categoryDto);
        }
    }
}
