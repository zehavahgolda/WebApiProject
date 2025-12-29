using Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using System.Threading.Tasks;
using Xunit;

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
            var order = new Order { OrderId = 1};

            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(order);
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

            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);
            var repository = new OrderrRepository(mockContext.Object);
            var order = new Order {OrderId = 2 };

            // Act
            await repository.AddOrder(order);

            // Assert
            mockSet.Verify(m => m.AddAsync(order, default), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}