using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Categories
{
    public class PutTests : BaseTests
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
            CategoryLogicMock
                .Verify( x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public void Return_BadRequest_When_Category_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var categoryDto = Builder<CategoryDto>.CreateNew().Build();
            var category = Builder<Category>.CreateNew().Build();   
            var errorMessage = "BadRequest";
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                 Returns(Result.Ok(category));
            MapperMock
                .Setup(x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()));
            CategoryLogicMock
                .Setup(r => r.Update(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(category.Name, errorMessage));

            //Act
            var result = logic.Put(categoryDto.Id, categoryDto);

            //Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock.Verify(
                x => x.GetById(categoryDto.Id), Times.Once());

            CategoryLogicMock.Verify(
                x => x.Update(category), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()), Times.Once());  
        }

        [Fact]
        public void Return_Created_Category_When_Category_is_Valid()
        {
            //Arrange
            var logic = Create();
            var categoryDto = Builder<CategoryDto>.CreateNew().Build();
            var category = Builder<Category>.CreateNew().Build();
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(category));
            MapperMock
                .Setup(x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()));
            CategoryLogicMock
                .Setup(r => r.Update(It.IsAny<Category>()))
                .Returns(Result.Ok(category));
            MapperMock
                .Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(categoryDto);

            //Act
            var result = logic.Put(categoryDto.Id, categoryDto);

            //Assert
            result.Should().BeOk(categoryDto);
            CategoryLogicMock.Verify(
                x => x.GetById(categoryDto.Id), Times.Once());

            CategoryLogicMock.Verify(
                x => x.Update(category), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Once());
        }
    }
}
