using Entity;
using System.Text.Json;
namespace Repository
{
    public class UserRepository
    {
         public string _filePath = "ListOfUsers.txt";

        public User GetUsersById(int id)
        {
            var lines = System.IO.File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var user = JsonSerializer.Deserialize<User>(line);
                if (user.id == id)
                    return user;
            }
            return null;
        }

        public User addUser(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines(_filePath).Count();
            user.id = numberOfUsers + 1;

            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(_filePath, userJson + Environment.NewLine);

            return user;

        }
        public void updateUser(int id, User user)
        {
            var lines = System.IO.File.ReadAllLines(_filePath).ToList();
            bool found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var user2 = JsonSerializer.Deserialize<User>(lines[i]);
                if (user.id == id)
                {
                    lines[i] = JsonSerializer.Serialize(user);
                    found = true;
                    break;
                }
            }

            if (found)
            {
                System.IO.File.WriteAllLines(_filePath, lines);

            }


        }


        public User login(User user)
        {
            var lines = System.IO.File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var user2 = JsonSerializer.Deserialize<User>(line);
                if (user.userName == user.userName && user.password == user.password)
                    return (user);
            }

            return null;
        }
    }
}
