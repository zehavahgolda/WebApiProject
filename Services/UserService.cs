using AutoMapper;
using DTOs;
using Entity;
using Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using BCrypt.Net; 
namespace Services
{
    public class UserService : IUserservice
    {
        private readonly IUserRepository _IuserRepository;
        private readonly IPasswordService _Ipasswordservice;
        private readonly IMapper _imapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IPasswordService passwordservice, IMapper imapper, IConfiguration configuration, ILogger<UserService> logger)
        {
            _IuserRepository = userRepository;
            _Ipasswordservice = passwordservice;
            _imapper = imapper;
            _configuration = configuration;
            _logger = logger;
        }

        private string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var token = new JwtSecurityToken(
                jwtSettings["Issuer"], jwtSettings["Audience"], claims,
                expires: DateTime.Now.AddHours(3), signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User?> addUserServices(User user)
        {
            _logger.LogInformation($"Adding user to repository: {user.Email}");
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return await _IuserRepository.AddUser(user);
        }

        public async Task<UserResponseDto?> addUserServices(UserRegisterDto userDto)
        {
            _logger.LogInformation($"Validating registration for: {userDto.Email}");
            int score = _Ipasswordservice.Level(userDto.Password).Strength;
            if (score < 2)
            {
                _logger.LogWarning($"Weak password for: {userDto.Email}");
                return null;
            }

            User user = _imapper.Map<User>(userDto);
            User newUser = await addUserServices(user);
            return _imapper.Map<UserResponseDto>(newUser);
        }

        public async Task<LoginResponseDto?> loginServices(UserLoginDto loginDto)
        {
            User userToLogin = _imapper.Map<User>(loginDto);
            User? authenticatedUser = await _IuserRepository.Login(userToLogin);

            if (authenticatedUser == null)
            {
                _logger.LogWarning($"Login failed for: {loginDto.Email}");
                return null;
            }

            string token = GenerateToken(authenticatedUser);
            return new LoginResponseDto(_imapper.Map<UserResponseDto>(authenticatedUser), token);
        }

        public async Task<UserResponseDto?> GetById(int id) => _imapper.Map<UserResponseDto>(await _IuserRepository.GetById(id));
        public async Task<IEnumerable<UserResponseDto>> GetUsers() => _imapper.Map<IEnumerable<UserResponseDto>>(await _IuserRepository.GetUsers());
        public async Task update(UserRegisterDto userDto, int id) => await _IuserRepository.Put(id, _imapper.Map<User>(userDto));
    }
}