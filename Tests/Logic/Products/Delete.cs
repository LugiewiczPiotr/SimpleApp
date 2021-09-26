using FizzWare.NBuilder;
using FluentAssertions;
using SimpleApp.Core.Models;
using System;
using Xunit;

namespace Tests.Logic.Products
{
    public class Delete : BaseTest
    {
        [Fact]
        public void Return_Error_When_Product_Is_Null()
        {
            var logic = Create();

            Action result = () => logic.Delete(null);

            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Return_Succes_When_Product_Is_Deleted()
        {
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();

            ProductRespositoryMock.Setup(r => r.Delete(product));
            var result = logic.Delete(product);

            result.Success.Should().BeTrue();
        }


    }
}
