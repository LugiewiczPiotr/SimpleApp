using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models;

namespace Tests.Logic.Categories
{
    public class BaseTest
    {
        protected Mock<ICategoryRepository> CategoryRepositoryMock { get; private set; }
        protected Mock<IProductRepository> ProductRespositoryMock { get; private set; }
        protected Mock<IValidator<Category>> ValidatorMock { get; private set; }

        protected CategoryLogic Create()
        {
            CategoryRepositoryMock = new Mock<ICategoryRepository>();
            ProductRespositoryMock = new Mock<IProductRepository>();
            ValidatorMock = new Mock<IValidator<Category>>();
            return new CategoryLogic(
                CategoryRepositoryMock.Object,
                ProductRespositoryMock.Object,
                ValidatorMock.Object);
        }
    }
}
