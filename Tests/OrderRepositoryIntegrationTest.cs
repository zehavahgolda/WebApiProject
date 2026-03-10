using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var repository = new OrderrRepository(_context);
            var order = new Order { OredrDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 200.0, UserId = 1, OrderStatus = "Pending" };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var result = await repository.GetOrderById(order.OrderId);

            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
        }

        [Fact]
        public async Task AddOrder_PersistsOrderToDatabase()
        {
            var repository = new OrderrRepository(_context);
            var order = new Order { OredrDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 250.0, UserId = 1, OrderStatus = "New" };

            var result = await repository.AddOrder(order);

            var retrievedOrder = await _context.Orders.FindAsync(result.OrderId);
            Assert.NotNull(retrievedOrder);
            Assert.Equal(250.0, retrievedOrder.OrderSum);
        }

        [Fact]
        public async Task UpdateStatus_UpdatesDatabaseCorrectly()
        {
            var repository = new OrderrRepository(_context);
            var order = new Order { OrderStatus = "Old Status", UserId = 1 };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await repository.UpdateStatus(order.OrderId, "Updated Status");

            var updatedOrder = await _context.Orders.FindAsync(order.OrderId);
            Assert.Equal("Updated Status", updatedOrder.OrderStatus);
        }

        [Fact]
        public async Task CalculateOrderSum_HappyPath_ReturnsCorrectSum()
        {
       
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: "HappyPathSumDb")
                .Options;

            using (var context = new Store_329391924Context(options))
            {
                var repository = new OrderrRepository(context);

                var product = new Product { Price = 50.0, Description = "Test Product" };
                context.Products.Add(product);
                await context.SaveChangesAsync();

                var order = new Order { UserId = 1, OrderStatus = "Processing" };
                context.Orders.Add(order);
                await context.SaveChangesAsync();

             
                var orderItem = new OrdeItem { OrderId = order.OrderId, ProductId = product.ProductId, Quantity = 2, Product = product };
                context.OrdeItems.Add(orderItem);
                await context.SaveChangesAsync();

                // Act
                double calculatedSum = await repository.CalculateOrderSum(order.OrderId);

                // Assert
                Assert.Equal(100.0, calculatedSum);
            }
        }

        [Fact]
        public async Task CalculateOrderSum_UnhappyPath_HandlesInvalidOrder()
        {
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: "UnhappyPathSumDb")
                .Options;

            using (var context = new Store_329391924Context(options))
            {
                var repository = new OrderrRepository(context);

                // Act 
                double calculatedSum = await repository.CalculateOrderSum(-999);

                // Assert
                Assert.Equal(0.0, calculatedSum);
            }
        }
    }
}