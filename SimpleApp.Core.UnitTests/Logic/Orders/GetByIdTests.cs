using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class GetByIdTests : BaseTests
    {
        [Fact]
        public async Task Return_Error_When_Order_Not_Exist()
        {
            // Arrange
            var logic = Create();
            OrderRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).
                ReturnsAsync((Order)null);
            var guid = Guid.NewGuid();

            // Act
            var result = await logic.GetByIdAsync(guid);

            // Assert
            result.Should().BeFailure($"Order with ID {guid} does not exist.");
            OrderRepositoryMock.Verify(
                x => x.GetByIdAsync(guid), Times.Once());
        }

        [Fact]
        public async Task Return_Order_From_Repository()
        {
            // Arrange
            var logic = Create();
            var order = Builder<Order>.CreateNew().Build();
            OrderRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).
                ReturnsAsync(order);

            // Act
            var result = await logic.GetByIdAsync(order.Id);

            // Assert
            result.Should().BeSuccess(order);
            OrderRepositoryMock.Verify(
                x => x.GetByIdAsync(order.Id), Times.Once());
        }
    }
}
