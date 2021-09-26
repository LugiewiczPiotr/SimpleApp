using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using Moq;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Logics;
using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Logic.Products
{
    public class Update
    {
        protected Mock<IProductRepository> ProductRespositoryMock { get; private set; }
        protected Mock<IValidator<Product>> ValidatorMock { get; private set; }

        protected ProductLogic Create()
        {
            ProductRespositoryMock = new Mock<IProductRepository>();
            ValidatorMock = new Mock<IValidator<Product>>();
            return new ProductLogic(
                ProductRespositoryMock.Object,
                ValidatorMock.Object);
        }

        [Fact]
        public void Return_Succes_When_Product_Is_Valid()
        {
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();

            ValidatorMock.SetValidationSuccess();
            var result = logic.Update(product);


            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(product);
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0);
        }

        [Fact]
        public void Return_Succes_When_Prodcut_Is_Not_Valid()
        {
            var logic = Create();
            var product = Builder<Product>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(product.Name, "Validation fail");

            var result = logic.Update(product);

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);

        }

        [Fact]
        public void Return_Error_When_Product_Is_Null()
        {

            var logic = Create();


            Action result = () => logic.Update(null);

            result.Should().Throw<ArgumentNullException>();

        }
    }
}
