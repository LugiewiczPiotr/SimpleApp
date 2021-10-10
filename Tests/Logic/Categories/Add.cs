using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using System;
using System.Linq;
using Xunit;

namespace Tests.Logic.Categories
{
    public class Add : BaseTest
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            //Arrange
            var logic = Create();

            //Act
            Action result = () => logic.Add(null);

            //Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
               x => x.Validate(null), Times.Never());

            CategoryRepositoryMock.Verify(
               x => x.Add(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Error_When_Category_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(category.Name, "Validation fail");
            

            //Act
            var result = logic.Add(category);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
            ValidatorMock.Verify(
                x => x.Validate(category), Times.Once());

            CategoryRepositoryMock.Verify(
               x => x.Add(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Succes_When_Category_Is_Valid()
        {
            //Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();

            //Act
            var result = logic.Add(category);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
            ValidatorMock.Verify(
               x => x.Validate(category), Times.Once);

            CategoryRepositoryMock.Verify(
               x => x.Add(It.IsAny<Category>()), Times.Once());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());


        }

       
    }
}
