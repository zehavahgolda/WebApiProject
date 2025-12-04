using Repository.Models;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserservice
    {
        Task<User> GetUserByidService(int id);
        Task<User> addUserServices(User user);
        Task<User> loginServices(User user);
        Task<int> update(User user, int id);
    }
}