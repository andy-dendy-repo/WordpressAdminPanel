using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WordpressClient.Data;

namespace WordpressClient
{
    public class Test
    {
        public WpUsers GetPass()
        {
            AdminDbContext context = new AdminDbContext();

            var a = context.WpUsers.FirstOrDefault();

            string password = "1111";

            string hash = a.UserPass;

            string hash2 = crypt_private(password, hash);

            return a;
        }
        public string crypt_private(string password, string setting)
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

            string hash = GetHash(salt+password);

            do
            {
                hash = GetHash(hash + password);
            }
            while (--count!=0);

            //output = setting.Substring(0, 12);
            //output += encode64(hash);

            return setting;
        }

        public string encode64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
