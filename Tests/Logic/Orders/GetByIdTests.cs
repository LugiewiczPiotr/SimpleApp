using System;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public void Return_Error_When_Order_Not_Exist()
        {
            // Arrange
            var logic = Create();
            OrderRepositoryMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns((Order)null);
            var guid = Guid.NewGuid();

            // Act
            var result = logic.GetById(guid);

            // Assert
            result.Should().BeFailure($"Order with ID {guid} does not exist.");
            OrderRepositoryMock.Verify(
                x => x.GetById(guid), Times.Once());
        }

        [Fact]
        public void Return_Order_From_Repository()
        {
            // Arrange
            var logic = Create();
            var order = Builder<Order>.CreateNew().Build();
            OrderRepositoryMock
                .Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns(order);

            // Act
            var result = logic.GetById(order.Id);

            // Assert
            result.Should().BeSuccess(order);
            OrderRepositoryMock.Verify(
                x => x.GetById(order.Id), Times.Once());
        }
    }
}
