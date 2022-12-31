using System.Threading.Tasks;
using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Categories
{
    public class GetAllActiveTests : BaseTests
    {
        [Fact]
        public async Task Return_All_Categories_From_Repository()
        {
            // Arrange
            var logic = Create();
            var categories = Builder<Category>.CreateListOfSize(10).Build();
            CategoryRepositoryMock
                .Setup(r => r.GetAllActiveAsync())
                .ReturnsAsync(categories);

            // Act
            var result = await logic.GetAllActiveAsync();

            // Assert
            result.Should().BeSuccess(categories);
            CategoryRepositoryMock.Verify(
                x => x.GetAllActiveAsync(), Times.Once());
        }
    }
}
