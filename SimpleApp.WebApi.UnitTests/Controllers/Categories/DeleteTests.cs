﻿using System;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.Controllers;
using Xunit;

namespace SimpleApp.WebApi.UnitTests.Controllers.Categories
{
    public class DeleteTests : BaseTests
    {
        private Category category;
        private void CorrectFlow()
        {
            category = Builder<Category>.CreateNew().Build();
            CategoryLogicMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .Returns(Result.Ok(category));
            CategoryLogicMock
                .Setup(r => r.Delete(It.IsAny<Category>())).Returns(Result.Ok());
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
            var result = controller.Delete(guid);

            // Assert
            result.Should().BeNotFound<Category>(errorMessage);
            CategoryLogicMock
                .Verify(x => x.GetById(guid), Times.Once());
            CategoryLogicMock
                .Verify(x => x.Delete(It.IsAny<Category>()), Times.Never());
        }

        [Fact]
        public void Return_BeBadRequest_When_Category_Is_Not_Valid()
        {
            // Arrange
            var errorMessage = "BadRequest";
            var controller = Create();
            CategoryLogicMock
                .Setup(r => r.Delete(It.IsAny<Category>()))
                .Returns(Result.Failure<Category>(category.Name, errorMessage));

            // Act
            var result = controller.Delete(category.Id);

            // Assert
            result.Should().BeBadRequest<Category>(errorMessage);
            CategoryLogicMock
                .Verify(x => x.GetById(category.Id), Times.Once());
            CategoryLogicMock
                .Verify(x => x.Delete(category), Times.Once());
        }

        [Fact]
        public void Return_NoContent_When_Category_Is_Deleted()
        {
            // Arrange
            var controller = Create();

            // Act
            var result = controller.Delete(category.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            CategoryLogicMock
                .Verify(x => x.GetById(category.Id), Times.Once());
            CategoryLogicMock
                .Verify(x => x.Delete(category), Times.Once());
        }
    }
}
