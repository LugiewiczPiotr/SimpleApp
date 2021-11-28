using AutoMapper;
using Moq;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.WebApi.Controllers;

namespace SimpleApp.Core.UnitTests.WebApi.Products
{
    public class BaseTests
    {
        protected Mock<IProductLogic> ProductLogicMock { get; set; }
        protected Mock<IMapper> MapperMock { get; set; }
        protected virtual ProductController Create()
        {
            ProductLogicMock = new Mock<IProductLogic>();
            MapperMock = new Mock<IMapper>();
            return new ProductController(
                ProductLogicMock.Object,
                MapperMock.Object
                );
        }
    }
}
