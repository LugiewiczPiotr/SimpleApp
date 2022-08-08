using Microsoft.AspNetCore.Mvc;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class PostTests : BaseTests
    {
        private Category category;
        private CategoryDto categoryDto;
        private void CorrectFlow()
        {
            category = Builder<Category>.CreateNew().Build();
            categoryDto = Builder<CategoryDto>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Category>(It.IsAny<CategoryDto>()))
               .Returns(category);
            CategoryLogicMock.Setup(x => x.Add(It.IsAny<Category>()))
                .Returns(Result.Ok(category));
            MapperMock.Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(categoryDto);
        }

        protected override CategoryController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        [Fact]
        public void Return_BeBadRequest_When_Category_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "validation fail";
            CategoryLogicMock
                .Setup(x => x.Add(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(category.Name, errorMessage));

            // Act
            var result = controller.Post(categoryDto);
            
            // Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            MapperMock.Verify(
               x => x.Map<Category>(categoryDto), Times.Once());

            CategoryLogicMock.Verify(
               x => x.Add(category), Times.Once());

            MapperMock.Verify(
              x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Category_Is_Valid()
        {
            // Arrange
            var controller = Create();
           
            // Act
            var result = controller.Post(categoryDto);

            // Assert
            result.Should().BeCreatedAtAction(categoryDto);
            MapperMock.Verify(
               x => x.Map<Category>(categoryDto), Times.Once());

            CategoryLogicMock.Verify(
               x => x.Add(category), Times.Once());

            MapperMock.Verify(
               x => x.Map<CategoryDto>(category), Times.Once());
        }
    }
}
