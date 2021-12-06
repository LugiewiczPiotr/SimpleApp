using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class GetTests : BaseTests
    {
        [Fact]
        public void Return_All_Products()
        {
            //Arrange
            var controller = Create();
            var products = Builder<Product>.CreateListOfSize(1).Build().AsEnumerable();
            var productsDto = Builder<ProductDto>.CreateListOfSize(1).Build();
            ProductLogicMock
                .Setup(r => r.GetAllActive())
                .Returns(Result.Ok(products));
            MapperMock
                .Setup(m => m.Map<IList<ProductDto>>(It.IsAny<Product>()))
                .Returns(productsDto);

            //Act
            var result = controller.Get();

            //Assert
            result.Should().BeOk(productsDto);
            ProductLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());
            MapperMock.Verify(
                x => x.Map<IList<ProductDto>>(products), Times.Once());
        }
    }
}
