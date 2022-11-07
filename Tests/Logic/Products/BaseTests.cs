using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models.Entity;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class BaseTests
    {
        protected Mock<IProductRepository> ProductRepositoryMock { get; private set; }
        protected Mock<IValidator<Product>> ValidatorMock { get; private set; }
        protected ProductLogic Create()
        {
            ProductRepositoryMock = new Mock<IProductRepository>();
            ValidatorMock = new Mock<IValidator<Product>>();
            return new ProductLogic(
                ProductRepositoryMock.Object,
                ValidatorMock.Object);
        }
    }
}
