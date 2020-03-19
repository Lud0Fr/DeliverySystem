using System;
using System.Security.Cryptography;
using System.Text;

namespace DeliverySystem.Tools
{
    public static class Crypto
    {
        public static string ComputeSHA1(this string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();

            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer));
            return hash.Replace("-", "");
        }
    }
}
