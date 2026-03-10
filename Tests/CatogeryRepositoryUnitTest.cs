using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CategoryRepositoryUnitTest
    {
        [Fact]
        public async Task GetCategories_ReturnsCategories()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: "CategoryTestDb")
                .Options;

            using var context = new Store_329391924Context(options);

            context.Categories.AddRange(
                new Category { CategoryName = "Electronics" },
                new Category { CategoryName = "Books" }
            );

            await context.SaveChangesAsync();

            var repository = new CatogeryRepsitory(context);

            // Act
            var result = await repository.GetCatogries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}