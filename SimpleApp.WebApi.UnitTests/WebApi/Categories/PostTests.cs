using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Categories
{
    public class PostTests : BaseTests
    {
        [Fact]
        public void Return_BeBadRequest_When_Category_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var categoryDto = Builder<CategoryDto>.CreateNew().Build();
            var category = Builder<Category>.CreateNew().Build();
            var errorMessage = "validation fail";
            MapperMock
                .Setup(x => x.Map<Category>(categoryDto))
                .Returns(category);
            CategoryLogicMock
                .Setup(x => x.Add(category))
                .Returns(Result.Failure<Category>(category.Name, errorMessage));

            //Act
            var result = logic.Post(categoryDto);
            
            //Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock.Verify(
               x => x.Add(category), Times.Once());

            MapperMock.Verify(
                x => x.Map<Category>(categoryDto), Times.Once());

            MapperMock.Verify(
              x => x.Map<CategoryDto>(category), Times.Never());
        }

        [Fact]
        public void Return_Created_When_Category_Is_Valid()
        {
            //Arrange
            var logic = Create();
            var categoryDto = Builder<CategoryDto>.CreateNew().Build();
            var category = Builder<Category>.CreateNew().Build();
            MapperMock.Setup(x => x.Map<Category>(categoryDto))
                .Returns(category);
            CategoryLogicMock.Setup(x => x.Add(category))
                .Returns(Result.Ok(category));
            MapperMock.Setup(x => x.Map<CategoryDto>(category))
                .Returns(categoryDto);

            //Act
            var result = logic.Post(categoryDto);

            //Assert
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
