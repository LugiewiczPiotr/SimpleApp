using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.Controllers;
using SimpleApp.WebApi.DTO;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class PutTests : BaseTests
    {
        private Category _category;
        private CategoryDto _categoryDto;

        [Fact]
        public async Task Return_NotFound_When_Category_Not_Exist()
        {
            // Arrange
            var controller = Create();
            var guid = Guid.NewGuid();
            var errorMessage = $"Category with ID {guid} does not exist.";
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Failure<Category>(errorMessage));

            // Act
            var result = await controller.Put(guid, _categoryDto);

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
        public async Task Return_BadRequest_When_Category_Is_Not_Valid()
        {
            // Arrange
            var controller = Create();
            var errorMessage = "BadRequest";
            CategoryLogicMock
                .Setup(r => r.Update(It.IsAny<Category>()))
                .ReturnsAsync(Result.Failure<Category>(_category.Name, errorMessage));

            // Act
            var result = await controller.Put(_categoryDto.Id, _categoryDto);

            // Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock.Verify(
                x => x.GetById(_categoryDto.Id), Times.Once());

            CategoryLogicMock.Verify(
                x => x.Update(_category), Times.Once());

            MapperMock.Verify(
                x => x.Map(_categoryDto, _category), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public async Task Return_Created_Category_When_Category_is_Valid()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = await controller.Put(_categoryDto.Id, _categoryDto);

            // Assert
            result.Should().BeOk(_categoryDto);
            CategoryLogicMock.Verify(
                x => x.GetById(_categoryDto.Id), Times.Once());

            CategoryLogicMock.Verify(
                x => x.Update(_category), Times.Once());

            MapperMock.Verify(
                x => x.Map(_categoryDto, _category), Times.Once());

            MapperMock.Verify(
                x => x.Map<CategoryDto>(_category), Times.Once());
        }

        protected override CategoryController Create()
        {
            var controller = base.Create();
            CorrectFlow();
            return controller;
        }

        private void CorrectFlow()
        {
            _categoryDto = Builder<CategoryDto>.CreateNew().Build();
            _category = Builder<Category>.CreateNew().Build();
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Result.Ok(_category));
            MapperMock
                .Setup(x => x.Map(It.IsAny<CategoryDto>(), It.IsAny<Category>()));
            CategoryLogicMock
                .Setup(r => r.Update(It.IsAny<Category>()))
                .ReturnsAsync(Result.Ok(_category));
            MapperMock
                .Setup(x => x.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(_categoryDto);
        }
    }
}
