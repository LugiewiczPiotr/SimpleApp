using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class GetTests : BaseTests
    {
        [Fact]
        public async Task Return_BadRequest_When_Products_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "BadRequest";
            var products = Builder<Product>.CreateListOfSize(1).Build().AsEnumerable();
            ProductLogicMock.Setup(x => x.GetAllActiveAsync())
                .ReturnsAsync(Result.Failure<IEnumerable<Product>>(errorMessage));

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeBadRequest<IEnumerable<Product>>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetAllActiveAsync(), Times.Once());

            MapperMock.Verify(
                x => x.Map<IList<ProductDto>>(It.IsAny<IEnumerable<Product>>()), Times.Never());
        }

        [Fact]
        public async Task Return_All_Products()
        {
            // Arrange
            var controller = Create();
            var products = Builder<Product>.CreateListOfSize(1).Build().AsEnumerable();
            var productsDto = Builder<ProductDto>.CreateListOfSize(1).Build();
            ProductLogicMock
                .Setup(r => r.GetAllActiveAsync())
                .ReturnsAsync(Result.Ok(products));
            MapperMock
                .Setup(m => m.Map<IList<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(productsDto);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOk(productsDto);
            ProductLogicMock.Verify(
                x => x.GetAllActiveAsync(), Times.Once());
            MapperMock.Verify(
                x => x.Map<IList<ProductDto>>(products), Times.Once());
        }
    }
}
