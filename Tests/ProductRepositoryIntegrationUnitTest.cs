//using Entity;
//using Repository;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Tests;
//using Xunit;

//namespace RepositoryIntegrationTests
//{
//    public class ProductRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
//    {
//        private readonly Store_329391924Context _context;

//        public ProductRepositoryIntegrationTests(DatabaseFixture fixture)
//        {
//            _context = fixture.Context;
//        }

//        [Fact]
//        //public async Task GetProducts_ReturnsAllProducts_FromDatabase()
//        //{
//        //    // Arrange
//        //    var repository = new ProductRepository(_context);
//        //    var product1 = new Product { ProductId = 1, ProductName = "Product1", Price = 10.0, CategoryId = 1, Description = "Description1" };
//        //    var product2 = new Product { ProductId = 2, ProductName = "Product2", Price = 20.0, CategoryId = 2, Description = "Description2" };
//        //    _context.Products.Add(product1);
//        //    _context.Products.Add(product2);
//        //    await _context.SaveChangesAsync();

//        //    // Act
//        //    var result = await repository.GetProducts(null, null, null, null, null,null);

//        //    // Assert
//        //    Assert.Equal(2, result.Count);
//        //    Assert.Contains(result, p => p.ProductName == "Product1" && p.Price == 10.0 && p.CategoryId == 1 && p.Description == "Description1");
//        //    Assert.Contains(result, p => p.ProductName == "Product2" && p.Price == 20.0 && p.CategoryId == 2 && p.Description == "Description2");
//        }
//    }
//}