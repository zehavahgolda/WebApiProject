using Microsoft.EntityFrameworkCore;
using Entity;
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
            await _store_329391924Context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(int id, User user)
        {
            
            user.Id = id;

            _store_329391924Context.Users.Update(user);
            await _store_329391924Context.SaveChangesAsync();
        }

        public async Task<User> Login(User user)
        {
            string email = user.Email.Trim();
            string password = user.Password.Trim();

            return await _store_329391924Context.Users
                .FirstOrDefaultAsync(x => x.Email.Trim() == email &&
                                           x.Password.Trim() == password);
        }
    }
}
