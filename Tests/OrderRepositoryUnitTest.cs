using System; // חובה עבור DateTime
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using Xunit;
using Entity;

namespace RepositoryTests
{
    public class OrderrRepositoryTests
    {
        [Fact]
        public async Task GetOrderById_ReturnsOrder_WhenExists()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<Store_329391924Context>();
            var order = new Order { OrderId = 1, OredrDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 100.0, UserId = 1 };

            // ב-Moq עבור EF Core, לפעמים צריך להגדיר את זה כך בגלל ש-FindAsync מקבל מערך של אובייקטים
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(order);
            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            var repository = new OrderrRepository(mockContext.Object);

            // Act
            var result = await repository.GetOrderById(1);

            // Assert
            Assert.Equal(order, result);
        }

        [Fact]
        public async Task AddOrder_AddsOrderSuccessfully()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<Store_329391924Context>();
            var order = new Order { OrderId = 2, OredrDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 150.0, UserId = 2 };

            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            mockSet.Setup(m => m.AddAsync(It.IsAny<Order>(), default)).ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Order>)null);

            var repository = new OrderrRepository(mockContext.Object);

            // Act
            var result = await repository.AddOrder(order);

            // Assert
            
            mockSet.Verify(m => m.AddAsync(order, default), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.Equal(order, result);
        }
    }
}