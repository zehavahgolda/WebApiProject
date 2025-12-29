using Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<Store_329391924Context>();
            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", Password = "password1" },
                new User { Id = 2, Email = "user2@example.com", FirstName = "Jane", LastName = "Doe", Password = "password2" }
            };

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            var repository = new UserRepository(mockContext.Object);

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
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<Store_329391924Context>();
            var user = new User { Id = 3, Email = "user3@example.com", FirstName = "Alice", LastName = "Smith", Password = "password3" };

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.AddUser(user);

            // Assert
            mockSet.Verify(m => m.AddAsync(user, default), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetById_ReturnsUser_WhenExists()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<Store_329391924Context>();
            var user = new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", Password = "password1" };

            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(user);
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.GetById(1);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<Store_329391924Context>();
            var user = new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", Password = "password1" };

            mockSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>(), default))
                   .ReturnsAsync(user);
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            var repository = new UserRepository(mockContext.Object);

            // Act
            var result = await repository.Login(new User { Email = "user1@example.com", Password = "password1" });

            // Assert
            Assert.Equal(user, result);
        }
    }
}