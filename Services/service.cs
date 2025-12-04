using Repository;
using Repository.Models;
using System.Threading.Tasks;

namespace Services
{
    public class Userservice : IUserservice
    {
        IUserRepository _IuserRepository;
        Ipasswordservice _Ipasswordservice;

        public Userservice(IUserRepository userRepository, Ipasswordservice passwordservice)
        {
            _IuserRepository = userRepository;
            _Ipasswordservice = passwordservice;
        }

        public async Task<User> GetUserByidService(int id)
        {
            return await _IuserRepository.GetUsersById(id);
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
            return await _IuserRepository.FindUser(user);
        }

        public async Task<int> update(User user, int id)
        {
            await _IuserRepository.updateUser(id, user);
            return 1;
            //צריך טיפןל דחוףףףףףף
        }
    }
}
