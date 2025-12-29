using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryIntegrationTests
{
    public class OrderrRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly Store_329391924Context _context;

        public OrderrRepositoryIntegrationTests(DatabaseFixture fixture)
        {
            _context = fixture.CreateContext();
        }

        [Fact]
        public async Task GetOrderById_ReturnsOrder_FromDatabase()
        {
            // Arrange
            var repository = new OrderrRepository(_context);
            var order = new Order { OrderId = 3};
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await repository.GetOrderById(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
        }

        [Fact]
        public async Task AddOrder_PersistsOrderToDatabase()
        {
            // Arrange
            var repository = new OrderrRepository(_context);
            var order = new Order { OrderId = 4 };

            // Act
            await repository.AddOrder(order);

            // Assert
            var result = await _context.Orders.FindAsync(4);
            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
        }
    }

    public class DatabaseFixture
    {
        public Store_329391924Context? Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
        .UseSqlServer("Server=desktop-t8jm6mu; Database=Store_329391924; Integrated Security=True; TrustServerCertificate=True;")
        .Options;

            Context = new Store_329391924Context(options);
            Context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        internal Store_329391924Context? CreateContext()
        {
            throw new NotImplementedException();
        }
    }
}
