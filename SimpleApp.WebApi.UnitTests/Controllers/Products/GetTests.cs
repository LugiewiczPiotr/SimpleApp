using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entity;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class GetTests : BaseTests
    {
        [Fact]
        public void Return_BadRequest_When_Products_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "BadRequest";
            var products = Builder<Product>.CreateListOfSize(1).Build().AsEnumerable();
            ProductLogicMock.Setup(x => x.GetAllActive())
                .Returns(Result.Failure<IEnumerable<Product>>(errorMessage));

            // Act
            var result = controller.Get();

            // Assert
            result.Should().BeBadRequest<IEnumerable<Product>>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());

            MapperMock.Verify(
                x => x.Map<IList<ProductDto>>(It.IsAny<IEnumerable<Product>>()), Times.Never());
        }

        [Fact]
        public void Return_All_Products()
        {
            // Arrange
            var controller = Create();
            var products = Builder<Product>.CreateListOfSize(1).Build().AsEnumerable();
            var productsDto = Builder<ProductDto>.CreateListOfSize(1).Build();
            ProductLogicMock
                .Setup(r => r.GetAllActive())
                .Returns(Result.Ok(products));
            MapperMock
                .Setup(m => m.Map<IList<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(productsDto);

            // Act
            var result = controller.Get();

            // Assert
            result.Should().BeOk(productsDto);
            ProductLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());
            MapperMock.Verify(
                x => x.Map<IList<ProductDto>>(products), Times.Once());
        }
    }
}
