using Microsoft.EntityFrameworkCore;
using Entity;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        Store_329391924Context _store_329391924Context;

        public UserRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }
        public async Task<User> GetUsersById(int id)
        {
            return await _store_329391924Context.Users.FindAsync(id);
        }

  
        public async Task<User> AddUser(User user)
        {
            await _store_329391924Context.Users.AddAsync(user);
            await _store_329391924Context.SaveChangesAsync();
            return user;
        }

        public async Task updateUser(int id, User user)
        {
            _store_329391924Context.Users.Update(user);
            await _store_329391924Context.SaveChangesAsync();
        }

        public async Task<User> Login(User user)
        {
            return await _store_329391924Context.Users.
               FirstOrDefaultAsync(x =>x.Email==user.
               Email && x.Password==user.Password);
        }
     

    }
}
