using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace RepositoryIntegrationTests
{
    public class ProductRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly Store_329391924Context _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryIntegrationTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _repository = new ProductRepository(_context);

            // ניקוי מסד הנתונים לפני כל טסט כדי להבטיח סביבה נקייה
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetProducts_WithFilters_ReturnsFilteredResultsAndTotalCount()
        {
            // Arrange
            var categoryId = 1;
            var products = new List<Product>
            {
                new Product { ProductName = "Matching Product", Price = 50.0, CategoryId = categoryId, IsActive = true, Quantity = 5 },
                new Product { ProductName = "Expensive Product", Price = 200.0, CategoryId = categoryId, IsActive = true, Quantity = 5 },
                new Product { ProductName = "Other Category", Price = 50.0, CategoryId = 2, IsActive = true, Quantity = 5 }
            };

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            // Act - חיפוש לפי קטגוריה ומחיר מקסימלי
            var (resultProducts, total) = await _repository.GetProducts(
                categoryId: new int[] { categoryId },
                q: null,
                minPrice: null,
                maxPrice: 100.0,
                color: null,
                material: null,
                inStock: true,
                isActive: true,
                sort: "asc",
                skip: 10,
                position: 1
            );

            // Assert
            Assert.Single(resultProducts); // רק מוצר אחד עונה על כל התנאים
            Assert.Equal(1, total);
            Assert.Equal("Matching Product", resultProducts.First().ProductName);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_SavesToDatabase()
        {
            // Arrange
            var newProduct = new Product
            {
                ProductName = "New Test Product",
                Price = 15.5,
                CategoryId = 1,
                IsActive = true
            };

            // Act
            var savedProduct = await _repository.AddProduct(newProduct);

            // Assert
            Assert.NotEqual(0, savedProduct.ProductId);
            var dbProduct = await _context.Products.FindAsync(savedProduct.ProductId);
            Assert.NotNull(dbProduct);
            Assert.Equal("New Test Product", dbProduct.ProductName);
        }

        [Fact]
        public async Task AddProduct_MissingName_ThrowsArgumentException()
        {
            // Arrange
            var invalidProduct = new Product { ProductName = "", CategoryId = 1 };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.AddProduct(invalidProduct));
        }

        [Fact]
        public async Task GetProductById_ExistingId_ReturnsProductWithCategory()
        {
            // Arrange
            var category = new Category { CategoryName = "Electronics" };
            var product = new Product { ProductName = "Laptop", Category = category, IsActive = true };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductById(product.ProductId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Laptop", result.ProductName);
            Assert.NotNull(result.Category); // בדיקה שה-Include עובד
        }

        [Fact]
        public async Task DeleteProduct_ExistingProduct_RemovesFromDb()
        {
            // Arrange
            var product = new Product { ProductName = "To Delete", CategoryId = 1 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteProduct(product.ProductId);
            var result = await _context.Products.FindAsync(product.ProductId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProduct_ExistingProduct_UpdatesDetails()
        {
            // Arrange
            var product = new Product { ProductName = "Old Name", Price = 10, CategoryId = 1 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            var updatedData = new Product { ProductName = "New Name", Price = 20, CategoryId = 1 };

            // Act
            await _repository.UpdateProduct(product.ProductId, updatedData);

            // Assert
            var result = await _context.Products.FindAsync(product.ProductId);
            Assert.Equal("New Name", result.ProductName);
            Assert.Equal(20, result.Price);
        }
    }
}