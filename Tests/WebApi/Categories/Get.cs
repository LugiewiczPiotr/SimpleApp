using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Categories
{
    public class Get : BaseTest
    {
        [Fact]
        public void Return_All_Categories()
        {
            //Arrange
            var controller = Create();
            var categories = Builder<Category>.CreateListOfSize(10).Build();
            var categoryDtos = Builder<CategoryDto>.CreateListOfSize(10).Build();
            CategoryLogicMock
                .Setup(r => r.GetAllActive())
                .Returns(Result.Ok(categories.AsEnumerable()));
            MapperMock
                .Setup(m => m.Map<IList<CategoryDto>>(categories.AsEnumerable()))
                .Returns(categoryDtos);

            //Act
            var result = controller.Get().Result;

            //Assert
            result.Should().BeOk(categoryDtos);
            CategoryLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());

            MapperMock.Verify(
                x => x.Map<IList<CategoryDto>>(categories.AsEnumerable()), Times.Once());   
        }
    }
}
