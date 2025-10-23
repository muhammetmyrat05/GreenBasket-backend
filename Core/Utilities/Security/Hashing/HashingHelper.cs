using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                byte[] salt = hmac.Key;  // 64 byte salt

                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // BYTE[] → STRING (Hex) - .NET 3.1
                passwordHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
                passwordSalt = BitConverter.ToString(salt).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            // STRING → BYTE[]
            byte[] hashBytes = HexStringToByteArray(passwordHash);
            byte[] saltBytes = HexStringToByteArray(passwordSalt);

            using (HMACSHA512 hmac = new HMACSHA512(saltBytes))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Timing attack korumasy
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hashBytes[i])
                        return false;
                }
                return computedHash.Length == hashBytes.Length;
            }
        }

        // HEX STRING → BYTE[] (.NET 3.1)
        private static byte[] HexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}