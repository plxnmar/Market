using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Helpers
{
    public static class HashPasswordHelper
    {
        public static string GetHashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var salt = "fer45bo23yh89f7w34bu3fq8ar";

                byte[] messageBytes = Encoding.UTF8.GetBytes(salt + password);
                var hashValue = sha256.ComputeHash(messageBytes);

                return Convert.ToHexString(hashValue);
            }

            return password;
        }
    }
}
