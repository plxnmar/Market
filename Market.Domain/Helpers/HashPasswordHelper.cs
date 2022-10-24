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
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] messageBytes = ue.GetBytes(password);
            SHA256 shHash = SHA256.Create();

            var hashValue = shHash.ComputeHash(messageBytes);

            return hashValue.ToString();
        }
    }
}
