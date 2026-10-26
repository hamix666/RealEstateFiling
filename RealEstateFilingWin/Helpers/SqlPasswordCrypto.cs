using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Helpers
{
    public static class SqlPasswordCrypto
    {
        private const int IvSize = 16;

        public static string EncryptWithRawKey(string plainText, string base64Key)
        {
            byte[] key = Convert.FromBase64String(base64Key);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs, Encoding.UTF8))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptWithRawKey(string cipherPackageBase64, string base64Key)
        {
            byte[] package = Convert.FromBase64String(cipherPackageBase64);
            byte[] key = Convert.FromBase64String(base64Key);

            byte[] iv = new byte[IvSize];
            Array.Copy(package, 0, iv, 0, IvSize);

            int cipherLen = package.Length - IvSize;
            byte[] cipher = new byte[cipherLen];
            Array.Copy(package, IvSize, cipher, 0, cipherLen);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (var ms = new MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
