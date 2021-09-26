using FizzWare.NBuilder;
using FluentAssertions;
using SimpleApp.Core.Models;
using System;
using Xunit;

namespace Tests.Logic.Categories
{
    public class Delete : BaseTest
    {
        [Fact]
        public void Return_Error_When_Category_Is_Null()
        {
            var logic = Create();

            Action result = () => logic.Delete(null);

            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Return_Succes_When_Category_Is_Deleted()
        {
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            var product = Builder<Product>.CreateNew().Build();
            category.Id = product.CategoryId;

            CategoryRepositoryMock.Setup(x => x.Delete(category));
            ProductRespositoryMock.Setup(r => r.DeleteByCategoryId(category.Id));

            var result = logic.Delete(category);

            result.Success.Should().BeTrue();
        }


    }
}
