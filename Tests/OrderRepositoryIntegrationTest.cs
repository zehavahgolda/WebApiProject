using Entity;
using Repository;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace RepositoryIntegrationTests
{
    public class OrderrRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly Store_329391924Context _context;

        public OrderrRepositoryIntegrationTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetOrderById_ReturnsOrder_FromDatabase()
        {
            // Arrange
            var repository = new OrderrRepository(_context);
            var order = new Order { OredrDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 200.0, UserId = 3 };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await repository.GetOrderById(order.OrderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
            Assert.Equal(order.OrderSum, result.OrderSum);
        }

        [Fact]
        public async Task AddOrder_PersistsOrderToDatabase()
        {
            // Arrange
            var repository = new OrderrRepository(_context);
            var order = new Order { OredrDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 250.0, UserId = 4 };

            // Act
            var result = await repository.AddOrder(order);

            // Assert
            var retrievedOrder = await _context.Orders.FindAsync(result.OrderId);
            Assert.NotNull(retrievedOrder);
            Assert.Equal(order.OrderSum, retrievedOrder.OrderSum);
        }
    }
}