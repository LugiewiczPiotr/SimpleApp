using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using SimpleApp.Core.Logics;
using FluentValidation;

namespace Tests.Logic.Products
{
    public class GetById : BaseTest
    {
        [Fact]
        public void Return_Product_From_Repository()
        {
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();

            ProductRespositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns(product);

            var result = logic.GetById(product.Id);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(product);
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
        }

        [Fact]
        public void Return_Error_When_Prodcut_Not_Exist()
        {
            var logic = Create();


            ProductRespositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).
                Returns((Product)null);

            var result = logic.GetById(Guid.NewGuid());

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
        }



    }
}
