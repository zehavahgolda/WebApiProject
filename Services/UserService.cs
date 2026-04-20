using AutoMapper;
using DTOs;
using Entity;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserservice
    {
        private readonly IUserRepository _IuserRepository;
        private readonly IPasswordService _Ipasswordservice;
        private readonly IMapper _imapper;

        public UserService(IUserRepository userRepository, IPasswordService passwordservice, IMapper imapper)
        {
            _IuserRepository = userRepository;
            _Ipasswordservice = passwordservice;
            _imapper = imapper;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsers()
        {
            var users = await _IuserRepository.GetUsers();
            return _imapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetById(int id)
        {
            var user = await _IuserRepository.GetById(id);
            return _imapper.Map<User, UserResponseDto>(user);
        }

        public async Task<UserResponseDto?> addUserServices(UserRegisterDto userDto)
        {
           
            int score = _Ipasswordservice.Level(userDto.Password).Strength;
            if (score < 2) return null;

            
            User user = _imapper.Map<User>(userDto);
            User newUser = await _IuserRepository.AddUser(user);

            return _imapper.Map<UserResponseDto>(newUser);
        }

        public async Task<UserResponseDto?> loginServices(UserLoginDto loginDto)
        {
            
            User userToLogin = _imapper.Map<User>(loginDto);
            User? authenticatedUser = await _IuserRepository.Login(userToLogin);

            if (authenticatedUser == null) return null;

            return _imapper.Map<UserResponseDto>(authenticatedUser);
        }

        public async Task update(UserRegisterDto userDto, int id)
        {
            User user = _imapper.Map<User>(userDto);
            await _IuserRepository.Put(id, user);
        }

        public Task<User> addUserServices(User user)
        {
            throw new NotImplementedException();
        }
    }
}