using Entity;
using Repository;
using System.Linq;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace RepositoryIntegrationTests
{
    public class UserRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly Store_329391924Context _context;

        public UserRepositoryIntegrationTests(DatabaseFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetUsers_ReturnsAllUsers_FromDatabase()
        {
            _context.Users.RemoveRange(_context.Users);
            await _context.SaveChangesAsync();

            // Arrange
            var repository = new UserRepository(_context);
            var user1 = new User
            {
                Email = "user1@example.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "password1",
                Role = "User"   // ✅ הוספנו Role
            };
            var user2 = new User
            {
                Email = "user2@example.com",
                FirstName = "Jane",
                LastName = "Doe",
                Password = "password2",
                Role = "User"   // ✅ הוספנו Role
            };
            _context.Users.Add(user1);
            _context.Users.Add(user2);
            await _context.SaveChangesAsync();

            // Act
            var result = await repository.GetUsers();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Email == "user1@example.com");
            Assert.Contains(result, u => u.Email == "user2@example.com");
        }

        [Fact]
        public async Task AddUser_PersistsUserToDatabase()
        {
            // Arrange
            var repository = new UserRepository(_context);
            var user = new User
            {
                Email = "user3@example.com",
                FirstName = "Alice",
                LastName = "Smith",
                Password = "password3",
                Role = "User"   
            };

            // Act
            await repository.AddUser(user);

            // Assert
            var result = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Login_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var repository = new UserRepository(_context);
            var user = new User
            {
                Email = "user4@example.com",
                FirstName = "Bob",
                LastName = "Johnson",
                Password = "password4",
                Role = "User"   
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await repository.Login(new User { Email = "user4@example.com", Password = "password4" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }
    }
}