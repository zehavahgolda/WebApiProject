using Entity;
using Repository;
using Services;


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


        public User GetUserByidService(int id)
        {
            return _IuserRepository.GetUsersById(id);
        }
        public void update(User user, int id)
        {
            _IuserRepository.updateUser(id, user);
        }
        public User addUserServices(User user)
        {
            int score = _Ipasswordservice.Level(user.password).Strength;
            if (score < 2)
                return null;
            return _IuserRepository.addUser(user);
        }
        public User loginServices(User user)
        {
            return _IuserRepository.login(user);
        }

    }
}
