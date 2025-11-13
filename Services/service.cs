using Entity;
using Repository;
using Services;


namespace Services
{
    public class Userservice
    {
        UserRepository repository = new UserRepository();
        passwordservice passwordservice = new passwordservice();    


        public User GetUserByidService(int id)
        {
            return repository.GetUsersById(id);
        }
        public void update(User user,int id) {
            repository.updateUser(id,user);
        }
        public User addUserServices(User user){
            int score = passwordservice.Level(user.password).Strength;
            if (score < 2)
                return null;
            return repository.addUser(user);
        }
        public User loginServices(User user){
            return repository.login(user);
        }

    }
}
