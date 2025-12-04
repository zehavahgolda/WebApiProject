using Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> FindUser(User user);
        Task<User> GetUsersById(int id);
        Task updateUser(int id, User user);
    }
}
