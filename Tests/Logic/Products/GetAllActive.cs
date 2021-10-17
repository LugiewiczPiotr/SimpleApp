using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core.Models;
using Xunit;
using SimpleApp.Core;

namespace Tests.Logic.Products
{
    public class GetAllActive : BaseTest
    {
        [Fact]
        public void Return_All_Products_From_Repository()
        {
            //Arrange
            var logic = Create();
            var products = Builder<Product>.CreateListOfSize(10).Build();
            ProductRespositoryMock.Setup(r => r.GetAllActive()).Returns(products);

            //Act
            var result = logic.GetAllActive();

            //Assert
            result.Should().BeSuccess(products);
            ProductRespositoryMock.Verify(
                x => x.GetAllActive(), Times.Once());
        }
    }
}
