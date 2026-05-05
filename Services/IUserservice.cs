using DTOs;
using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserservice
    {
        Task<UserResponseDto?> addUserServices(UserRegisterDto userDto);
        Task<User?> addUserServices(User user);
        Task<UserResponseDto?> GetById(int id);
        Task<IEnumerable<UserResponseDto>> GetUsers();
        Task<LoginResponseDto?> loginServices(UserLoginDto loginDto);
        Task update(UserRegisterDto userDto, int id);
    }
}