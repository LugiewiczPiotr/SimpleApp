using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class GetAllActiveTests : BaseTests
    {
        [Fact]
        public void Return_All_Orders_From_Repository()
        {
            // Arrange
            var logic = Create();
            var orders = Builder<Order>.CreateListOfSize(10).Build();
            OrderRepositoryMock
                .Setup(r => r.GetAllActive())
                .Returns(orders);

            // Act
            var result = logic.GetAllActive();

            // Assert
            result.Should().BeSuccess(orders);
            OrderRepositoryMock.Verify(
                x => x.GetAllActive(), Times.Once());
        }
    }
}
