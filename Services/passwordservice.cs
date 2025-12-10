using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PasswordService : Ipasswordservice
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
    }
}
