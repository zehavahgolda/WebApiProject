using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> Login(User user);
        Task<ActionResult<User>> Put(int id, User user);
    }
}