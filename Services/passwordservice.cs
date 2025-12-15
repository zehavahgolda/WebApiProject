using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PasswordService : IPasswordService
    {
        public passwordEntity Level(string pass)
        {
            var result = Zxcvbn.Core.EvaluatePassword(pass);
            int strength = result.Score;
            passwordEntity pass1 = new passwordEntity();
            pass1.Password = pass;
            pass1.Strength = strength;
            return pass1;
        }
        private const int MIN_REQUIRED_STRENGTH = 3;
        public bool UpdatePassword(int userId, string newPassword)
        {
            var strengthResult = Level(newPassword);

            if (strengthResult.Strength < MIN_REQUIRED_STRENGTH)
            {
                return false;
            }
            return true;
        }
    }
}
