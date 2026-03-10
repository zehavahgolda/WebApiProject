using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTests
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<Store_329391924Context> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: "Test_Store_" + Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetProducts_ReturnsAllActiveProducts_WhenNoFiltersApplied()
        {
            // Arrange
            var options = GetInMemoryOptions();

            using (var context = new Store_329391924Context(options))
            {
                context.Products.AddRange(new List<Product>
                {
                    new Product { ProductId = 1, ProductName = "Laptop", Price = 1000, IsActive = true, Quantity = 10 },
                    new Product { ProductId = 2, ProductName = "Mouse", Price = 50, IsActive = true, Quantity = 5 },
                    new Product { ProductId = 3, ProductName = "Hidden Item", Price = 10, IsActive = false, Quantity = 0 }
                });
                await context.SaveChangesAsync();
            }

            // Act
            
            using (var context = new Store_329391924Context(options))
            {
                var repository = new ProductRepository(context);

                var result = await repository.GetProducts(null, null, null, null, null, null, null, true, null, null, null);

                // Assert
              
                Assert.Equal(2, result.total); 
                Assert.Contains(result.products, p => p.ProductName == "Laptop");
                Assert.Contains(result.products, p => p.ProductName == "Mouse");
            }
        }

        [Fact]
        public async Task GetProducts_FiltersByPrice_ReturnsCorrectProducts()
        {
            // Arrange
            var options = GetInMemoryOptions();
            using (var context = new Store_329391924Context(options))
            {
                context.Products.AddRange(new List<Product>
                {
                    new Product { ProductId = 1, ProductName = "Cheap", Price = 10, IsActive = true },
                    new Product { ProductId = 2, ProductName = "Medium", Price = 50, IsActive = true },
                    new Product { ProductId = 3, ProductName = "Expensive", Price = 100, IsActive = true }
                });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new Store_329391924Context(options))
            {
                var repository = new ProductRepository(context);

                // סינון מחיר בין 40 ל-60
                var result = await repository.GetProducts(null, null, 40.0, 60.0, null, null, null, true, null, null, null);

                // Assert
                Assert.Single(result.products); // רק מוצר אחד בטווח
                Assert.Equal("Medium", result.products.First().ProductName);
            }
        }

        [Fact]
        public async Task AddProduct_ValidProduct_SavesSuccessfully()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var newProduct = new Product
            {
                ProductName = "New Product",
                Price = 99.9,
                CategoryId = 1,
                IsActive = true
            };

            // Act
            using (var context = new Store_329391924Context(options))
            {
                var repository = new ProductRepository(context);
                var savedProduct = await repository.AddProduct(newProduct);
            }

            // Assert
            using (var context = new Store_329391924Context(options))
            {
                Assert.Equal(1, await context.Products.CountAsync());
                Assert.Equal("New Product", (await context.Products.FirstAsync()).ProductName);
            }
        }
    }
}