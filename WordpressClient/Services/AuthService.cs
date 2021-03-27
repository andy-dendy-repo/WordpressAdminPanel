using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WordpressClient.Data;
using WordpressClient.Services.Interfaces;

namespace WordpressClient.Services
{
    public class AuthService : IAuthService
    {
        private const string itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public bool SignIn(string username, string password)
        {
            AdminDbContext context = new AdminDbContext();

            var foundUser = context.WpUsers.FirstOrDefault(x => x.UserLogin == username);

            if (foundUser != null)
            {
                string foundUserHash = foundUser.UserPass;

                string hash = Crypt(password, foundUserHash);

                return foundUserHash == hash;
            }
            else
                return false;
        }

        private string Crypt(string password, string setting)
        {


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

            var hash = GetHash(
                GetByteArraysAppended(
                    Encoding.UTF7.GetBytes(salt),
                    Encoding.UTF7.GetBytes(password)
                    ));
            
            do
            {
                hash = GetHash(
                    GetByteArraysAppended(
                        hash, 
                        Encoding.UTF7.GetBytes(password)
                        ));
            }
            while (--count!=0);

            output = setting.Substring(0, 12);

            output += encode64(hash, 16);

            return output;
        }

        private string encode64(byte [] input, int count)
        {
            string output = "";
            int i = 0;

            do
            {
                Int32 value = input[i++];
                output += itoa64[value & 0x3f];

                if (i < count)
                    value |= input[i] << 8;
                output += itoa64[(value >> 6) & 0x3f];
                if (i++ >= count)
                    break;
                if (i < count)
                    value |= input[i] << 16;
                output += itoa64[(value >> 12) & 0x3f];
                if (i++ >= count)
                    break;
                output += itoa64[(value >> 18) & 0x3f];
            } while (i < count);

            return output;
        }



        private byte[] GetByteArraysAppended(byte[] partOne, byte[] partTwo)
        {
            var parts = partOne.ToList();
            parts.AddRange(partTwo);
            var result = parts.ToArray();

            return result;
        }

        private byte[] GetHash(byte [] bytesToHash)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(bytesToHash);

            return hash;
        }
    }
}
