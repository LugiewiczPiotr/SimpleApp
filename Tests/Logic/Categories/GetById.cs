using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;

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
            

            //Arrange
            var result = logic.GetById(Guid.NewGuid());

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
            CategoryRepositoryMock.Verify(
                x => x.GetById(Guid.NewGuid()), Times.Never()); 
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
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(category);
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
            CategoryRepositoryMock.Verify(
                x => x.GetById(category.Id), Times.Once()); 
        }
    }
}
