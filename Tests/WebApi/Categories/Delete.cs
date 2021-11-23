using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Categories
{
    public class Delete : BaseTest
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
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Return_BeBadRequest_When_Category_Is_Not_Valid()
        {
            //Arrange
            var errorMessage ="BadRequest";
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(category));
            CategoryLogicMock
                .Setup(r => r.Delete(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(category.Name, errorMessage));

            //Act
            var result = logic.Delete(category.Id);

            //Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
            CategoryLogicMock
                .Verify(x => x.Delete(category), Times.Once());
        }

        [Fact]
        public void Return_NoContent_When_Category_Is_Deleted()
        {
            //Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(category));
            CategoryLogicMock
                .Setup(r => r.Delete(category)).Returns(Result.Ok());
                

            //Act
            var result = logic.Delete(category.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
            CategoryLogicMock
                .Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
            CategoryLogicMock
                .Verify(x => x.Delete(category), Times.Once());
        }
    }
}
