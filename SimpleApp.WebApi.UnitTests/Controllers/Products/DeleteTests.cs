using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.Controllers;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class DeleteTests : BaseTests
    {
        private Product _product;

        [Fact]
        public async Task Return_NotFound_When_Product_Not_Exist()
        {
            // Arrange
            var controller = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Failure<Product>(errorMessage));

            // Act
            var result = await controller.Delete(guid);

            // Assert
            result.Should().BeNotFound<Product>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(guid), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public async Task Return_BeBadRequest_When_Product_Is_Not_Valid()
        {
            // Arrange
            var errorMessage = "BadRequest";
            var controller = Create();
            ProductLogicMock
                .Setup(r => r.Delete(It.IsAny<Product>()))
                .Returns(Result.Failure<Category>(_product.Name, errorMessage));

            // Act
            var result = await controller.Delete(_product.Id);

            // Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(_product.Id), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(_product), Times.Once());
        }

        [Fact]
        public async Task Return_NoContent_When_Product_Is_Deleted()
        {
            // Arrange
            var logic = Create();

            // Act
            var result = await logic.Delete(_product.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            ProductLogicMock
                .Verify(x => x.GetById(_product.Id), Times.Once());
            ProductLogicMock
                .Verify(x => x.Delete(_product), Times.Once());
        }

        protected override ProductController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        private void CorrectFlow()
        {
            _product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Ok(_product));
            ProductLogicMock
                .Setup(r => r.Delete(It.IsAny<Product>())).Returns(Result.Ok());
        }
    }
}
