using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using SimpleApp.Core;

namespace Tests.Logic.Products
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
                x => x.Validate(It.IsAny<Product>()), Times.Never());

            ProductRespositoryMock.Verify(
               x => x.Add(It.IsAny<Product>()), Times.Never());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Error_When_Product_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(product.Name, "Validation fail");

            //Act
            var result = logic.Add(product);

            //Assert
            result.Should().BeFailure("Product is not valid");
            ValidatorMock.Verify(
               x => x.Validate(product), Times.Once());

            ProductRespositoryMock.Verify(
               x => x.Add(It.IsAny<Product>()), Times.Never());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Succes_When_Product_Is_Valid()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();

            //Act
            var result = logic.Add(product);

            //Assert
            result.Should().BeSuccess(product);
            ValidatorMock.Verify(
              x => x.Validate(product), Times.Once());

            ProductRespositoryMock.Verify(
               x => x.Add(It.IsAny<Product>()), Times.Once());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }

       

        

    }
}
