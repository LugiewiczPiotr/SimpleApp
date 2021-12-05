using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Categories
{
    public class PostTests : BaseTests
    {
        private Category Category;
        private CategoryDto CategoryDto;
        private void CorrectFlow()
        {
            Category = Builder<Category>.CreateNew().Build();
            CategoryDto = Builder<CategoryDto>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Category>(It.IsAny<CategoryDto>()))
               .Returns(Category);
            CategoryLogicMock.Setup(x => x.Add(It.IsAny<Category>()))
                .Returns(Result.Ok(Category));
            MapperMock.Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(CategoryDto);

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
            //Arrange
            var logic = Create();
            var errorMessage = "validation fail";
            CategoryLogicMock
                .Setup(x => x.Add(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(Category.Name, errorMessage));

            //Act
            var result = logic.Post(CategoryDto);
            
            //Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock.Verify(
               x => x.Add(Category), Times.Once());

            MapperMock.Verify(
                x => x.Map<Category>(CategoryDto), Times.Once());

            MapperMock.Verify(
              x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Category_Is_Valid()
        {
            //Arrange
            var logic = Create();
           
            //Act
            var result = logic.Post(CategoryDto);

            //Assert
            result.Should().BeCreatedAtAction(CategoryDto);
            MapperMock.Verify(
               x => x.Map<Category>(CategoryDto), Times.Once());

            CategoryLogicMock.Verify(
               x => x.Add(Category), Times.Once());

            MapperMock.Verify(
               x => x.Map<CategoryDto>(Category), Times.Once());
        }

    }
}
