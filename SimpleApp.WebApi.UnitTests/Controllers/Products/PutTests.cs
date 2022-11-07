using System;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entity;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class PutTests : BaseTests
    {
        private Product _product;
        private ProductDto _productDto;

        [Fact]
        public void Return_NotFound_When_Product_Not_Exist()
        {
            // Arrange
            var logic = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Product>(errorMessage));

            // Act
            var result = logic.Put(guid, _productDto);

            // Assert
            result.Should().BeNotFound<Product>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(guid), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()), Times.Never());

            ProductLogicMock.Verify(
                x => x.Update(It.IsAny<Product>()), Times.Never());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public void Return_BadRequest_When_Product_Is_Not_Valid()
        {
            // Arrange
            var logic = Create();
            var errorMessage = "BadRequest";
            ProductLogicMock
                .Setup(r => r.Update(It.IsAny<Product>()))
                .Returns(Result.Failure<Product>(_product.Name, errorMessage));

            // Act
            var result = logic.Put(_productDto.Id, _productDto);

            // Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetById(_productDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(_product), Times.Once());

            MapperMock.Verify(
                x => x.Map(_productDto, _product), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public void Return_Created_Category_When_Product_is_Valid()
        {
            // Arrange
            var logic = Create();

            // Act
            var result = logic.Put(_productDto.Id, _productDto);

            // Assert
            result.Should().BeOk(_productDto);
            ProductLogicMock.Verify(
                x => x.GetById(_productDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(_product), Times.Once());

            MapperMock.Verify(
                x => x.Map(_productDto, _product), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(_product), Times.Once());
        }

        protected override ProductController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        private void CorrectFlow()
        {
            _productDto = Builder<ProductDto>.CreateNew().Build();
            _product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(_product));
            MapperMock
                .Setup(x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()));
            ProductLogicMock
                .Setup(r => r.Update(It.IsAny<Product>()))
                .Returns(Result.Ok(_product));
            MapperMock
                .Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(_productDto);
        }
    }
}
