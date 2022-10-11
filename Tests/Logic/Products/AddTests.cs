﻿using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class AddTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Action result = () => logic.Add(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
                x => x.Validate(It.IsAny<Product>()), Times.Never());

            ProductRepositoryMock.Verify(
               x => x.Add(It.IsAny<Product>()), Times.Never());

            ProductRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Failure_When_Product_Is_Not_Valid()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            const string errorMessage = "validation fail";
            ValidatorMock.SetValidationFailure(product.Name, errorMessage);

            // Act
            var result = logic.Add(product);

            // Assert
            result.Should().BeFailure(property: product.Name, message: errorMessage);
            ValidatorMock.Verify(
               x => x.Validate(product), Times.Once());

            ProductRepositoryMock.Verify(
               x => x.Add(It.IsAny<Product>()), Times.Never());

            ProductRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Succes_When_Product_Is_Valid()
        {
            // Arrange
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();

            // Act
            var result = logic.Add(product);

            // Assert
            result.Should().BeSuccess(product);
            ValidatorMock.Verify(
              x => x.Validate(product), Times.Once());

            ProductRepositoryMock.Verify(
               x => x.Add(product), Times.Once());

            ProductRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }
    }
}
