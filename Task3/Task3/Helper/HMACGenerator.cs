using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Task3.Helper
{
    public class HMACGenerator
    {
        private RandomNumberGenerator secureRandomGenerator;

        public HMACGenerator()
        {
            secureRandomGenerator = RandomNumberGenerator.Create();
        }

        public static string ConvertKeyToHex(byte[] key)
        {
            return BitConverter.ToString(key).Replace("-", "");
        }

        public static byte[] ConvertHexToKey(string hexkey)
        {
            return Enumerable.Range(0, hexkey.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hexkey.Substring(x, 2), 16))
                     .ToArray();
        }

        public byte[] GenerateRandomKey(int keySize)
        {
            byte[] key = new byte[keySize];
            secureRandomGenerator.GetBytes(key);
            return key;
        }

        public string CalculateHMAC(byte[] key, string message)
        {
            HMACSHA256 hmac = new HMACSHA256(key);
            string hmacKey = ConvertKeyToHex(hmac.ComputeHash(Encoding.UTF8.GetBytes(message)));
            return hmacKey;
        }
    }
}
