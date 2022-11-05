using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class BaseTests
    {
        protected Mock<IOrderRepository> OrderRepositoryMock { get; private set; }
        protected Mock<IValidator<Order>> ValidatorMock { get; private set; }
        protected OrderLogic Create()
        {
            OrderRepositoryMock = new Mock<IOrderRepository>();
            ValidatorMock = new Mock<IValidator<Order>>();
            return new OrderLogic(
                OrderRepositoryMock.Object,
                ValidatorMock.Object);
        }
    }
}
