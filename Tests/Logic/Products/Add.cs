using FizzWare.NBuilder;
using FluentAssertions;
using SimpleApp.Core.Models;
using System;
using System.Linq;
using Xunit;

namespace Tests.Logic.Products
{
    public class Add : BaseTest
    {
        [Fact]
        public void Return_Succes_When_Product_Is_Valid()
        {
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();
            var result = logic.Add(product);


            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
        }

        [Fact]
        public void Return_Error_When_Product_Is_Not_Valid()
        {
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(product.Name, "Validation fail");

            var result = logic.Add(product);

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Return_Error_When_Product_Is_Null()
        {

            var logic = Create();

            Action result = () => logic.Add(null);

            result.Should().Throw<ArgumentNullException>();
        }


    }
}
