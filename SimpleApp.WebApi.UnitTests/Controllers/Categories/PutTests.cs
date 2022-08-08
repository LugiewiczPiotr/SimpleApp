using System;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class PutTests : BaseTests
    {
        private Category category;
        private CategoryDto categoryDto;
        private void CorrectFlow()
        {
            categoryDto = Builder<CategoryDto>.CreateNew().Build();
            category = Builder<Category>.CreateNew().Build();
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(category));
            MapperMock
                .Setup(x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()));
            CategoryLogicMock
                .Setup(r => r.Update(It.IsAny<Category>()))
                .Returns(Result.Ok(category));
            MapperMock
                .Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(categoryDto);
        }

        protected override CategoryController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        [Fact]
        public void Return_NotFound_When_Category_Not_Exist()
        {
            // Arrange
            var controller = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Category with ID {guid} does not exist.";
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Failure<Category>(errorMessage));

            // Act
            var result = controller.Put(guid, categoryDto);

            // Assert
            result.Should().BeNotFound<Category>(errorMessage);
            CategoryLogicMock
                .Verify(x => x.GetById(guid), Times.Once());

            MapperMock.Verify(
                x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()), Times.Never());

            CategoryLogicMock.Verify(
               x => x.Update(It.IsAny<Category>()), Times.Never());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_BadRequest_When_Category_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "BadRequest";
            CategoryLogicMock
                .Setup(r => r.Update(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(category.Name, errorMessage));

            // Act
            var result = controller.Put(categoryDto.Id, categoryDto);

            // Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock.Verify(
                x => x.GetById(categoryDto.Id), Times.Once());

            CategoryLogicMock.Verify(
                x => x.Update(category), Times.Once());

            MapperMock.Verify(
                x => x.Map(categoryDto, category), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_Created_Category_When_Category_is_Valid()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = controller.Put(categoryDto.Id, categoryDto);

            // Assert
            result.Should().BeOk(categoryDto);
            CategoryLogicMock.Verify(
                x => x.GetById(categoryDto.Id), Times.Once());

            CategoryLogicMock.Verify(
                x => x.Update(category), Times.Once());

            MapperMock.Verify(
                x => x.Map(categoryDto, category), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(category), Times.Once());
        }
    }
}
