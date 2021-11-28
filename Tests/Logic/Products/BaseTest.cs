using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class BaseTests
    {
        protected Mock<IProductRepository> ProductRespositoryMock { get; private set; }
        protected Mock<IValidator<Product>> ValidatorMock { get; private set; }
        protected ProductLogic Create()
        {
            ProductRespositoryMock = new Mock<IProductRepository>();
            ValidatorMock = new Mock<IValidator<Product>>();
            return new ProductLogic(
                ProductRespositoryMock.Object,
                ValidatorMock.Object);
        }
    }
}
