using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Xunit;

namespace RepositoryIntegrationTests
{
    public class CatogeryRepositoryIntegrationTests
    {
        private Store_329391924Context GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: "TestDb_Categories")
                .Options;

            return new Store_329391924Context(options);
        }

        [Fact]
        public async Task GetCatogries_ReturnsCategories_FromDatabase()
        {
            // Arrange
            using var context = GetInMemoryDbContext();

            context.Categories.Add(new Category { CategoryName = "Electronics" });
            context.Categories.Add(new Category { CategoryName = "Books" });
            await context.SaveChangesAsync();

            var repository = new CatogeryRepsitory(context);

            // Act
            var result = await repository.GetCatogries();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Electronics");
            Assert.Contains(result, c => c.CategoryName == "Books");
        }
    }
}