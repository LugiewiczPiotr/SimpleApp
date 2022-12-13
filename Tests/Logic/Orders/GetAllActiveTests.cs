using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class GetAllActiveTests : BaseTests
    {
        [Fact]
        public async Task Return_All_Orders_From_Repository()
        {
            // Arrange
            var logic = Create();
            var orders = Builder<Order>.CreateListOfSize(10).Build();

            var orderId = orders.Cast<Order>()
                .FirstOrDefault(x => x.UserId != null);
            OrderRepositoryMock
                .Setup(r => r.GetAllActiveOrders(orderId.UserId))
                .ReturnsAsync(orders);

            // Act
            var result = await logic.GetAllActiveOrders(orderId.UserId);

            // Assert
            result.Should().BeSuccess(orders);
            OrderRepositoryMock.Verify(
                x => x.GetAllActiveOrders(orderId.UserId), Times.Once());
        }
    }
}
