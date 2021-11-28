using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleApp.Core.UnitTests.WebApi.Products
{
    public class Get : BaseTests
    {
        [Fact]
        public void Return_All_Products()
        {
            //Arrange
            var controller = Create();
            var products = Builder<Product>.CreateListOfSize(10).Build();
            var productsDto = Builder<ProductDto>.CreateListOfSize(10).Build();
            ProductLogicMock
                .Setup(r => r.GetAllActive())
                .Returns(Result.Ok(products.AsEnumerable()));
            MapperMock
                .Setup(m => m.Map<IList<ProductDto>>(products.AsEnumerable()))
                .Returns(productsDto);

            //Act
            var result = controller.Get().Result;

            //Assert
            result.Should().BeOk(productsDto);
            ProductLogicMock.Verify(
                x => x.GetAllActive(), Times.Once());
            MapperMock.Verify(
                x => x.Map<IList<ProductDto>>(products.AsEnumerable()), Times.Once());
        }
    }
}
