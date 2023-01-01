using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Products
{
    public class GetAllActiveTests : BaseTests
    {
        [Fact]
        public async Task Return_All_Products_From_Repository()
        {
            // Arrange
            var logic = Create();
            var products = Builder<Product>.CreateListOfSize(10).Build();
            ProductRepositoryMock
                .Setup(r => r.GetAllActiveAsync()).ReturnsAsync(products);

            // Act
            var result = await logic.GetAllActiveAsync();

            // Assert
            result.Should().BeSuccess(products);
            ProductRepositoryMock.Verify(
                x => x.GetAllActiveAsync(), Times.Once());
        }
    }
}
