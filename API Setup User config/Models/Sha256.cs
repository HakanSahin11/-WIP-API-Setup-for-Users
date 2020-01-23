using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API_Setup_User_config.Models
{
    public class Sha256
    {
        public static string Sha256Hash(string input, string saltByte)
        {
            byte[] salt = Convert.FromBase64String(saltByte);
            using SHA256 hash = SHA256Managed.Create();
                 return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(input + salt))
                .Select(item => item.ToString("x2")));
        }
    }
}
