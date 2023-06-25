using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace WelnessWebsite.Functions
{
    public class MyFunctions
    {
        public string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashed = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashed);
            }
        }
    }
