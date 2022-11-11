using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class UpdateTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Action result = () => logic.Update(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            ValidatorMock.Verify(
                x => x.Validate(It.IsAny<Order>()), Times.Never());

            OrderRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Failure_When_Order_Is_Not_Valid()
        {
            // Arrange
            var logic = Create();
            var order = Builder<Order>.CreateNew().Build();
            const string errorMessage = "validation fail";
            ValidatorMock.SetValidationFailure(order.Id.ToString(), errorMessage);

            // Act
            var result = logic.Update(order);

            // Assert
            result.Should().BeFailure(property: order.Id.ToString(), message: errorMessage);
            ValidatorMock.Verify(
                x => x.Validate(order), Times.Once());

            OrderRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Success_When_Order_Is_Valid()
        {
            // Arrange
            var logic = Create();
            var order = Builder<Order>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();

            // Act
            var result = logic.Update(order);

            // Assert
            result.Should().BeSuccess(order);
            ValidatorMock.Verify(
                x => x.Validate(order), Times.Once());

            OrderRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }
    }
}
