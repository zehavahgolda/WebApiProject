using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public class passwordservice
    {
        public passwordEntity Level(string pass)
        {
            var result = Zxcvbn.Core.EvaluatePassword(pass);
            int levelpass = result.Score;
            passwordEntity passRes = new passwordEntity();
            passRes.Password= pass;
            passRes.Strength = levelpass;
            return passRes;
        }

    }
}
