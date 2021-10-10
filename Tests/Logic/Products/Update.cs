using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Logic.Products
{
    public class Update : BaseTest
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            //Arrange
            var logic = Create();

            //Act
            Action result = () => logic.Update(null);

            //Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
                x => x.Validate(null), Times.Never());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Succes_When_Prodcut_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(product.Name, "Validation fail");

            //Act
            var result = logic.Update(product);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
            ValidatorMock.Verify(
                x => x.Validate(product), Times.Once());

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
            var result = logic.Update(product);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(product);
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
            ValidatorMock.Verify(
                x => x.Validate(product), Times.Once());

            ProductRespositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }

       

       
    }
}
