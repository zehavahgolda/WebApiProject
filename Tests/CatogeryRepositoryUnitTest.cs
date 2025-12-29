using Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CatogeryRepositoryUnitTest
    {
        private Mock<Store_329391924Context> mockContext;

        [Fact]
        public async Task GetCatogries_ReturnsCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CatogeryId = 1, CatogeryName = "Electronics" },
                new Category { CatogeryId = 2, CatogeryName = "Books" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Category>>();

      
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        
            mockContext = new Mock<Store_329391924Context>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

            var repository = new CatogeryRepsitory(mockContext.Object);

            // Act
            var result = await repository.GetCatogries();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}