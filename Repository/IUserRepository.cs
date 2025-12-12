using Entity;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> Login(User user);
        Task UpdateUser(int id, User user);
    }
}