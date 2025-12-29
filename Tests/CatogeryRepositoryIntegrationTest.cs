using Entity;
using Repository;
using Xunit;
using Tests;

namespace RepositoryIntegrationTests
{
    public class CatogeryRepositoryIntegrationTests
        : IClassFixture<DatabaseFixture>
    {
        private readonly Store_329391924Context _dbContext;

        public CatogeryRepositoryIntegrationTests(DatabaseFixture fixture)
        {
            _dbContext = fixture.Context;
        }

        [Fact]
        public async Task GetCatogries_ReturnsCategories_FromDatabase()
        {
            // Arrange
            _dbContext.Categories.Add(new Category
            {
                CatogeryId = 1,
                CatogeryName = "Electronics"
            });

            _dbContext.Categories.Add(new Category
            {
                CatogeryId = 2,
                CatogeryName = "Books"
            });

            await _dbContext.SaveChangesAsync();

            var repository = new CatogeryRepsitory(_dbContext);

            // Act
            var result = await repository.GetCatogries();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
