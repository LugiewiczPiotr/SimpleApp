using FizzWare.NBuilder;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using System;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Products
{
    public class PutTests : BaseTests
    {
        private Product Product;
        private ProductDto ProductDto;
        private void CorrectFlow()
        {
            ProductDto = Builder<ProductDto>.CreateNew().Build();
            Product = Builder<Product>.CreateNew().Build();
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(Product));
            MapperMock
                .Setup(x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()));
            ProductLogicMock
                .Setup(r => r.Update(It.IsAny<Product>()))
                .Returns(Result.Ok(Product));
            MapperMock
                .Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(ProductDto);
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
            var logic = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Product with ID {guid} does not exist.";
            ProductLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Product>(errorMessage));

            //Act
            var result = logic.Put(guid, ProductDto);

            //Assert
            result.Should().BeNotFound<Product>(errorMessage);
            ProductLogicMock
                .Verify(x => x.GetById(guid), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(It.IsAny<Product>()), Times.Never());

            MapperMock.Verify(
                x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()), Times.Never());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public void Return_BadRequest_When_Product_Is_Not_Valid()
        {
            //Arrange
            var logic = Create();
            var errorMessage = "BadRequest";
            ProductLogicMock
                .Setup(r => r.Update(It.IsAny<Product>()))
                .Returns(Result.Failure<Product>(Product.Name, errorMessage));

            //Act
            var result = logic.Put(ProductDto.Id, ProductDto);

            //Assert
            result.Should().BeBadRequest<Product>(errorMessage);
            ProductLogicMock.Verify(
                x => x.GetById(ProductDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(Product), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public void Return_Created_Category_When_Product_is_Valid()
        {
            //Arrange
            var logic = Create();

            //Act
            var result = logic.Put(ProductDto.Id, ProductDto);

            //Assert
            result.Should().BeOk(ProductDto);
            ProductLogicMock.Verify(
                x => x.GetById(ProductDto.Id), Times.Once());

            ProductLogicMock.Verify(
                x => x.Update(Product), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<ProductDto>(), It.IsAny<Product>()), Times.Once());

            MapperMock.Verify(
                x => x.Map<ProductDto>(Product), Times.Once());
        }
    }
}
