using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.UnitTests.Logic.Categories
{
    public class BaseTests
    {
        protected Mock<ICategoryRepository> CategoryRepositoryMock { get; private set; }
        protected Mock<IProductRepository> ProductRepositoryMock { get; private set; }
        protected Mock<IValidator<Category>> ValidatorMock { get; private set; }
        protected CategoryLogic Create()
        {
            CategoryRepositoryMock = new Mock<ICategoryRepository>();
            ProductRepositoryMock = new Mock<IProductRepository>();
            ValidatorMock = new Mock<IValidator<Category>>();
            return new CategoryLogic(
                CategoryRepositoryMock.Object,
                ProductRepositoryMock.Object,
                ValidatorMock.Object);
        }
    }
}
