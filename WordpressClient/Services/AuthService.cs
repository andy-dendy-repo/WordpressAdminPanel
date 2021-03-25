using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class AuthService : IAuthService
    {
        public bool SignIn(string username, string password)
        {
            AdminDbContext context = new AdminDbContext();

            var foundUser = context.WpUsers.FirstOrDefault(x=>x.UserLogin==username);

            string foundUserHash = foundUser.UserPass;

            string hash = Crypt(password, foundUserHash);

            return foundUserHash == hash;
        }

        private string Crypt(string password, string setting)
        {
            string itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            string output = "*0";

            if (setting.Substring(0, 2) == output)
                output = "*1";

            string id = setting.Substring(0, 3);

            if (id != "$P$" && id != "$H$")
                return output;

            int count_log2 = itoa64.IndexOf(setting[3]);

            if (count_log2 < 7 || count_log2 > 30)
                return output;

            var count = 1 << count_log2;

            string salt = setting.Substring(4, 8);

            if (salt.Length != 8)
                return output;

            string hash = GetHash(salt + password);

            do
            {
                hash = GetHash(hash + password);
            }
            while (--count != 0);

            return setting;
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
