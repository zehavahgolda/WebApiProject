//using Entity;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Repository;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace RepositoryTests
//{
//    public class ProductRepositoryTests
//    {
//        [Fact]
//        public async Task GetProducts_ReturnsListOfProducts()
//        {
//            // Arrange
//            var mockSet = new Mock<DbSet<Product>>();
//            var mockContext = new Mock<Store_329391924Context>();
//            var products = new List<Product>
//            {
//                new Product { ProductId = 1, ProductName = "Product1", Price = 10.0, CategoryId = 1, Description = "Description1" },
//                new Product { ProductId = 2, ProductName = "Product2", Price = 20.0, CategoryId = 2, Description = "Description2" }
//            };

//            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
//            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.AsQueryable().Expression);
//            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.AsQueryable().ElementType);
//            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

//            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

//            var repository = new ProductRepository(mockContext.Object);

//            // Act
//            var result = await repository.GetProducts(null, null, null, null, null);

//            // Assert
//            Assert.Equal(2, result.Count);
//            Assert.Equal("Product1", result[0].ProductName);
//            Assert.Equal(10.0, result[0].Price);
//            Assert.Equal(1, result[0].CategoryId);
//            Assert.Equal("Description1", result[0].Description);
//            Assert.Equal("Product2", result[1].ProductName);
//            Assert.Equal(20.0, result[1].Price);
//            Assert.Equal(2, result[1].CategoryId);
//            Assert.Equal("Description2", result[1].Description);
//        }
//    }
//}
