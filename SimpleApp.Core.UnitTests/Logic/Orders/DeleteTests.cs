using System;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using SimpleApp.Core;
using SimpleApp.Core.Models.Entities;
using Xunit;

namespace SimpleApp.Core.UnitTests.Logic.Orders
{
    public class DeleteTests : BaseTests
    {
        [Fact]
        public void Throw_ArgumentNullException_When_Argument_Is_Null()
        {
            // Arrange
            var logic = Create();

            // Act
            Func<Task> result = async () => await logic.DeleteAsync(null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
            OrderRepositoryMock.Verify(
                x => x.Delete(It.IsAny<Order>()), Times.Never());

            OrderRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async Task Return_Success_When_Order_Is_Deleted()
        {
            // Arrange
            var logic = Create();
            var order = Builder<Order>.CreateNew().Build();
            OrderRepositoryMock.Setup(
                x => x.Delete(order));

            // Act
            var result = await logic.DeleteAsync(order);

            // Assert
            result.Success.Should().BeTrue();
            OrderRepositoryMock.Verify(
                x => x.Delete(order), Times.Once());

            OrderRepositoryMock.Verify(
                x => x.SaveChangesAsync(), Times.Once());
        }
    }
}
