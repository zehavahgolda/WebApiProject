using Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Store_329391924Context _store_329391924Context;

        public UserRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _store_329391924Context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _store_329391924Context.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            await _store_329391924Context.Users.AddAsync(user);
            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = "User";
            }
            await _store_329391924Context.SaveChangesAsync();
            await _store_329391924Context.SaveChangesAsync();
            return user;
        }

        public async Task<ActionResult<User>> Put(int id, [FromBody] User updatedUser)
        {
          
            var existingUser = await _store_329391924Context.Users.FindAsync(id);
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Address = updatedUser.Address;
           
            await _store_329391924Context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<User> Login(User user)
        {
          
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }

            string email = user.Email.Trim();
            string password = user.Password;

            return await _store_329391924Context.Users
                .FirstOrDefaultAsync(x => x.Email.Trim() == email && x.Password == password);
        }
    }
}
