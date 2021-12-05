using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Categories
{
    public class Get_IdTests : BaseTests
    {
        [Fact]
        public void Return_NotFound_When_Category_Not_Exist()
        {
            //Arrange
            var logic = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Category with ID {guid} does not exist.";
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Category>(errorMessage));

            //Act
            var result = logic.Get(guid);

            //Assert
            result.Should().BeNotFound<Category>(errorMessage);
            CategoryLogicMock.Verify(
                x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public void Return_Ok_Category_When_Category_is_Exist()
        {
            //Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            var categoryDto = Builder<CategoryDto>.CreateNew().Build();
            CategoryLogicMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(category));
            MapperMock.Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(categoryDto);

            //Act
            var result = logic.Get(category.Id);

            //Assert
            result.Should().BeOk(categoryDto);
            CategoryLogicMock.Verify(
                x => x.GetById(category.Id), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(category), Times.Once());
        }
    }
}
