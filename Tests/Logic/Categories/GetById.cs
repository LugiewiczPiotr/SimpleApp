using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Models;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Tests.Logic.Categories
{
    public class GetById : BaseTest
    {
        [Fact]
        public void Return_Category_From_Repository()
        {
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();

            CategoryRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns(category);

            var result = logic.GetById(category.Id);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(category);
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
        }

        [Fact]
        public void Return_Error_When_Category_Not_Exist()
        {
            var logic = Create();
            

            CategoryRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns((Category)null);
            
            var result = logic.GetById(Guid.NewGuid());

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
        }



    }
}
