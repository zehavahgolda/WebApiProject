using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Store_329391924Context _storeContext;

        public UserRepository(Store_329391924Context storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _storeContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _storeContext.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "User";
            }

            await _storeContext.Users.AddAsync(user);
            await _storeContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Put(int id, User updatedUser)
        {
            var existingUser = await _storeContext.Users.FindAsync(id);
            if (existingUser == null) return null;

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Address = updatedUser.Address;
            existingUser.Email = updatedUser.Email; 

            await _storeContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> Login(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }

            string email = user.Email.Trim();
            string password = user.Password;

            return await _storeContext.Users
                .FirstOrDefaultAsync(x => x.Email.Trim() == email && x.Password == password);
        }
    }
}