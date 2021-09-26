using FizzWare.NBuilder;
using FluentAssertions;
using SimpleApp.Core.Models;
using System;
using System.Linq;
using Xunit;

namespace Tests.Logic.Categories
{
    public class Add : BaseTest
    {
        [Fact]
        public void Return_Succes_When_Category_Is_Valid()
        {
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationSuccess();
            var result = logic.Add(category);

            
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(0); 
        }

        [Fact]
        public void Return_Error_When_Category_Is_Not_Valid()
        {
            var logic = Create();
            var category = Builder<Category>.CreateNew().Build();
            ValidatorMock.SetValidationFailure(category.Name, "Validation fail");

            var result = logic.Add(category);

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNull();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Return_Error_When_Category_Is_Null()
        {
            
            var logic = Create();
            
            Action result = () => logic.Add(null);

            result.Should().Throw<ArgumentNullException>();
        }


    }
}
