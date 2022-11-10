using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class GetTests : BaseTests
    {
        [Fact]
        public void Return_BadRequest_When_Categories_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "BadRequest";
            var categories = Builder<Category>.CreateListOfSize(1).Build().AsEnumerable();
            CategoryLogicMock.Setup(x => x.GetAllActive())
                .Returns(Result.Failure<IEnumerable<Category>>(errorMessage));

            // Act
            var result = controller.Get();

            // Assert
            result.Should().BeBadRequest<IEnumerable<Category>>(errorMessage);
            CategoryLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());

            MapperMock.Verify(
                x => x.Map<IList<CategoryDto>>(It.IsAny<IEnumerable<Category>>()), Times.Never());
        }

        [Fact]
        public void Return_All_Categories()
        {
            // Arrange
            var controller = Create();
            var categories = Builder<Category>.CreateListOfSize(1).Build().AsEnumerable();
            var categoryDtos = Builder<CategoryDto>.CreateListOfSize(1).Build();
            CategoryLogicMock
                .Setup(r => r.GetAllActive())
                .Returns(Result.Ok(categories));
            MapperMock
                .Setup(m => m.Map<IList<CategoryDto>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(categoryDtos);

            // Act
            var result = controller.Get();

            // Assert
            result.Should().BeOk(categoryDtos);
            CategoryLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());

            MapperMock.Verify(
                x => x.Map<IList<CategoryDto>>(categories), Times.Once());
        }
    }
}
