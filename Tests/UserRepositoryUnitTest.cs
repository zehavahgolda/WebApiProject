using Xunit;
using Microsoft.EntityFrameworkCore;
using Repository;
using Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryTests
{
    public class UserRepositoryTests
    {
      
        private Store_329391924Context GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<Store_329391924Context>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            return new Store_329391924Context(options);
        }

        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", Password = "password1" },
                new User { Id = 2, Email = "user2@example.com", FirstName = "Jane", LastName = "Doe", Password = "password2" }
            };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetUsers();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Email == "user1@example.com");
            Assert.Contains(result, u => u.Email == "user2@example.com");
        }

        [Fact]
        public async Task AddUser_AddsUserSuccessfully()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = new User { Id = 3, Email = "user3@example.com", FirstName = "Alice", LastName = "Smith", Password = "password3" };

            // Act
            var result = await repository.AddUser(user);

            // Assert
            var userInDb = await context.Users.FindAsync(user.Id);
            Assert.NotNull(userInDb);
            Assert.Equal("User", userInDb.Role); // default role
            Assert.Equal(user.Email, userInDb.Email);
        }

        [Fact]
        public async Task GetById_ReturnsUser_WhenExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var user = new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", Password = "password1" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetById(1);

            // Assert
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var user = new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", Password = "password1" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);

            // Act
            var result = await repository.Login(new User { Email = "user1@example.com", Password = "password1" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }
    }
}