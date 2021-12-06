using AutoMapper;
using Moq;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.WebApi.Controllers;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class BaseTests
    {
        protected Mock<ICategoryLogic> CategoryLogicMock { get; set; }
        protected Mock<IMapper> MapperMock { get; set; }
        protected virtual CategoryController Create()
        {
            CategoryLogicMock = new Mock<ICategoryLogic>();
            MapperMock = new Mock<IMapper>();
            return new CategoryController(
                CategoryLogicMock.Object,
                MapperMock.Object
                );
        }
    }
}
