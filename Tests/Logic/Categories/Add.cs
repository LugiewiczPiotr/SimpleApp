using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using SimpleApp.Core;

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
               x => x.Validate(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
               x => x.Add(It.IsAny<Category>()), Times.Never());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Failure_When_Category_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(category.Name, "validation fail");

            //Act
            var result = logic.Add(category);

            //Assert
            result.Should().BeFailure(property:category.Name, message:"validation fail");
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
            result.Should().BeSuccess(category);
            ValidatorMock.Verify(
               x => x.Validate(category), Times.Once);

            CategoryRepositoryMock.Verify(
               x => x.Add(category), Times.Once());

            CategoryRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());


        }

       
    }
}
