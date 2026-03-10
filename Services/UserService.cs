using AutoMapper;
using DTOs;
using Entity    ;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserservice
    {
        IUserRepository _IuserRepository;
        IPasswordService _Ipasswordservice;
        IMapper _imapper;

        public UserService(IUserRepository userRepository, IPasswordService passwordservice, IMapper imapper)
        {
            _IuserRepository = userRepository;
            _Ipasswordservice = passwordservice;
            _imapper = imapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            IEnumerable<User> users = await _IuserRepository.GetUsers();
            IEnumerable<UserDto> usersDto = _imapper.Map<IEnumerable<User>,IEnumerable<UserDto>>(users);
            return usersDto;
        }
        public async Task<UserDto> GetById(int id)
        {
            User user = await _IuserRepository.GetById(id);
            UserDto userDto = _imapper.Map<User,UserDto>(user);
            return userDto;
        }

        public async Task<User> addUserServices(User user)
        {
            int score = _Ipasswordservice.Level(user.Password).Strength;
            if (score < 2)
                return null;

            return await _IuserRepository.AddUser(user);
        }

        public async Task<User> loginServices(User user)
        {
            return await _IuserRepository.Login(user);
        }

        public async Task update(UserDto userDto, int id)
        {
            
            User user = _imapper.Map<User>(userDto);
            await _IuserRepository.Put(id, user);
        }
    }
}
