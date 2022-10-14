using System;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class DeleteTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Action result = () => logic.Delete(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            OrderRepositoryMock.Verify(
                x => x.Delete(It.IsAny<Order>()), Times.Never());

            OrderRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Return_Success_When_Order_Is_Deleted()
        {
            // Arrange
            var logic = Create();
            var order = Builder<Order>.CreateNew().Build();
            OrderRepositoryMock.Setup(
                x => x.Delete(order));

            // Act
            var result = logic.Delete(order);

            // Assert
            result.Success.Should().BeTrue();
            OrderRepositoryMock.Verify(
                x => x.Delete(order), Times.Once());

            OrderRepositoryMock.Verify(
                x => x.SaveChanges(), Times.Once());
        }
    }
}
