using FizzWare.NBuilder;
using FluentAssertions;
using SimpleApp.Core.Models;
using Xunit;

namespace Tests.Logic.Categories
{
    public class GetAllActive : BaseTest
    {
        [Fact]
        public void Return_All_Categories_From_Repository()
        {
            var logic = Create();
            var categories = Builder<Category>.CreateListOfSize(10).Build();

            CategoryRepositoryMock.Setup(r => r.GetAllActive()).Returns(categories);

            var result = logic.GetAllActive();


            result.Success.Should().BeTrue();
            result.Errors.Should().NotBeNull();


        }
    }
}
