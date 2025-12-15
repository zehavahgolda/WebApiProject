using Entity;

namespace Services
{
    public interface IPasswordService
    {
        passwordEntity Level(string pass);
        bool UpdatePassword(int userId, string newPassword);
    }
}