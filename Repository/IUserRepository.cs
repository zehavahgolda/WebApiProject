using Entity;

namespace Repository
{
    public interface IUserRepository
    {
        User addUser(User user);
        User GetUsersById(int id);
        User login(User user);
        void updateUser(int id, User user);
    }
}