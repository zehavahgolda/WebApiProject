using Entity;

namespace Services
{
    public interface IUserservice
    {
        User addUserServices(User user);
        User GetUserByidService(int id);
        User loginServices(User user);
        void update(User user, int id);
    }
}