﻿using System;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class PutTests : BaseTests
    {
        private Product product;
        private ProductDto productDto;
        private void CorrectFlow()
        {
            productDto = Builder<ProductDto>.CreateNew().Build();
            product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(product));
            MapperMock
                .Setup(x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()));
            ProductLogicMock
                .Setup(r => r.Update(It.IsAny<Product>()))
                .Returns(Result.Ok(product));
            MapperMock
                .Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(productDto);
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
            // Arrange
            var logic = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Product>(errorMessage));

            // Act
            var result = logic.Put(guid, productDto);

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
                .Returns(Result.Failure<Product>(product.Name, errorMessage));

            // Act
            var result = logic.Put(productDto.Id, productDto);

            // Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetById(productDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(product), Times.Once());

            MapperMock.Verify(
                x => x.Map(productDto, product), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public void Return_Created_Category_When_Product_is_Valid()
        {
            // Arrange
            var logic = Create();

            // Act
            var result = logic.Put(productDto.Id, productDto);

            // Assert
            result.Should().BeOk(productDto);
            ProductLogicMock.Verify(
                x => x.GetById(productDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(product), Times.Once());

            MapperMock.Verify(
                x => x.Map(productDto, product), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(product), Times.Once());
        }
    }
}
