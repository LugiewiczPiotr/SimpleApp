using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using System;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class DeleteTests : BaseTests
    {
        private Product Product;
        private void CorrectFlow()
        {
            Product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(Product));
            ProductLogicMock
                .Setup(r => r.Delete(It.IsAny<Product>())).Returns(Result.Ok());
        }
        protected override ProductController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        [Fact]
        public void Return_NotFound_When_Product_Not_Exist()
        {
            //Arrange
            var controller = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Product>(errorMessage));

            //Act
            var result = controller.Delete(guid);

            //Assert
            result.Should().BeNotFound<Product>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(guid), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public void Return_BeBadRequest_When_Product_Is_Not_Valid()
        {
            //Arrange
            var errorMessage = "BadRequest";
            var controller = Create();
            ProductLogicMock
                .Setup(r => r.Delete(It.IsAny<Product>()))
                .Returns(Result.Failure<Category>(Product.Name, errorMessage));

            //Act
            var result = controller.Delete(Product.Id);

            //Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(Product.Id), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(Product), Times.Once());
        }

        [Fact]
        public void Return_NoContent_When_Product_Is_Deleted()
        {
            //Arrange
            var logic = Create();

            //Act
            var result = logic.Delete(Product.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
            ProductLogicMock
                .Verify(x => x.GetById(Product.Id), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(Product), Times.Once());
        }
    }
}
