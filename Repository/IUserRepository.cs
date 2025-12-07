using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> Login(User user);
        Task<User> GetUsersById(int id);
        Task updateUser(int id, User user);
    }
}
