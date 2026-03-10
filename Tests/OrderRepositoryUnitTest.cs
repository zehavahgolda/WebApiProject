using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using Xunit;
using Entity;
using System.Threading;

namespace RepositoryTests
{
    public class OrderrRepositoryTests
    {
        [Fact]
        public async Task GetOrderById_ReturnsOrder_WhenExists()
        {
            // Arrange
            var mockContext = new Mock<Store_329391924Context>();
            var order = new Order { OrderId = 1, OrderSum = 100.0, UserId = 1 };

            // הגדרה נכונה של FindAsync עבור Mock
            mockContext.Setup(c => c.Orders.FindAsync(It.IsAny<object[]>()))
                       .ReturnsAsync(order);

            var repository = new OrderrRepository(mockContext.Object);

            // Act
            var result = await repository.GetOrderById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
        }

        [Fact]
        public async Task AddOrder_AddsOrderSuccessfully()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<Store_329391924Context>();
            var order = new Order { OrderId = 2, OrderSum = 150.0, UserId = 2 };

            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            var repository = new OrderrRepository(mockContext.Object);

            // Act
            var result = await repository.AddOrder(order);

            // Assert
            mockSet.Verify(m => m.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(order, result);
        }
    }
}