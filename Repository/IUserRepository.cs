using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);

        Task<User?> GetById(int id);

        Task<IEnumerable<User>> GetUsers();

        Task<User?> Login(User user);

        Task<User?> Put(int id, User user);
    }
}