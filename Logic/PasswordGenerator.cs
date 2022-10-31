using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Logic
{
    public class PasswordGenerator
    {
        public Storage storage = Storage.GetStorage();
        private List<char> _garbageSymbols = new List<char> { '/', '\\', '=', '+', '-', '*' };

        public PasswordGenerator()
        {
            
        }

        public string Generate(string masterPassword, string website, string login)
        {
            website = website.ToLower();

            Encoding enc = Encoding.UTF8;

            List<byte> salt = new List<byte>(enc.GetBytes(masterPassword));
            salt.AddRange(enc.GetBytes(website).ToList());
            salt.AddRange(enc.GetBytes(login).ToList());

            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(salt.ToArray());

            Password newPwd = new Password(website, login, Convert.ToBase64String(result));
            storage.Passwords.Add(newPwd);

            return CleanPassword(Convert.ToBase64String(result));
        }

        public string CleanPassword(string pwd)
        {
            StringBuilder cleanedString = new StringBuilder();
            for(int i = 0; i < pwd.Length; i++)
            {
                if (!_garbageSymbols.Contains(pwd[i]))
                {
                    cleanedString.Append(pwd[i]);
                }
            }

            return cleanedString.ToString();
        }
    }
}