using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using FluentAssertions;
using SimpleApp.Core;

namespace Tests.Logic.Categories
{
    public class GetById : BaseTest
    {
        [Fact]
        public void Return_Error_When_Category_Not_Exist()
        {
            //Act
            var logic = Create();
            CategoryRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns((Category)null);
            var guid = Guid.NewGuid();


            //Arrange
            var result = logic.GetById(guid);

            //Assert
            result.Should().BeFailure("Category not exist ");
            CategoryRepositoryMock.Verify(
                x => x.GetById(guid), Times.Once()); 
        }

        [Fact]
        public void Return_Category_From_Repository()
        {
            //Act
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            CategoryRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns(category);

            //Arrange
            var result = logic.GetById(category.Id);

            //Assert
            result.Should().BeSuccess(category);
            CategoryRepositoryMock.Verify(
                x => x.GetById(category.Id), Times.Once()); 
        }
    }
}
