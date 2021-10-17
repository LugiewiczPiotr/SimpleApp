using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using SimpleApp.Core;

namespace Tests.Logic.Categories
{
    public class Update : BaseTest
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            //Act
            var logic = Create();

            //Arrange
            Action result = () => logic.Update(null);

            //Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
                x => x.Validate(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }
        [Fact]
        public void Return_Failure_When_Category_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(category.Name, "Validation fail");

            //Act
            var result = logic.Update(category);

            //Assert
            result.Should().BeFailure("Category is not valid");
            ValidatorMock.Verify(
                x => x.Validate(category), Times.Once());

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
            var result = logic.Update(category);

            //Assert
            result.Should().BeSuccess(category);
            ValidatorMock.Verify(
                x => x.Validate(category), Times.Once());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }

       

       
    }
}
