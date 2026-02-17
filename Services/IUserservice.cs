using DTOs;
using Entity;

namespace Services
{
    public interface IUserservice
    {
        Task<User> addUserServices(User user);
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<User> loginServices(User user);
        Task update(UserDto userDto, int id); 
    }
}