using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace CustomerConsoleApp.Helpers
{
    public static class EncryptionUtility
    {
        private static readonly string EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];


        public static string Encrypt(string text)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = Convert.FromBase64String(EncryptionKey);
                byte[] iv = aes.IV;

                // Generate a random salt for this password
                byte[] salt = new byte[16];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        byte[] textBytes = Encoding.UTF8.GetBytes(text);
                        cs.Write(textBytes, 0, textBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    // Combine the IV and ciphertext
                    byte[] fullCipher = new byte[iv.Length + ms.ToArray().Length];
                    Array.Copy(iv, 0, fullCipher, 0, iv.Length);
                    Array.Copy(ms.ToArray(), 0, fullCipher, iv.Length, ms.ToArray().Length);

                    return Convert.ToBase64String(fullCipher);
                }
            }
        }

        public static string Decrypt(string text)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = Convert.FromBase64String(EncryptionKey);

                byte[] fullCipher = Convert.FromBase64String(text);
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = new byte[fullCipher.Length - iv.Length];
                        Array.Copy(fullCipher, iv.Length, plainBytes, 0, plainBytes.Length);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        private static byte[] GenerateRandomIV()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[16];
                rng.GetBytes(iv);
                return iv;
            }
        }
    }
}