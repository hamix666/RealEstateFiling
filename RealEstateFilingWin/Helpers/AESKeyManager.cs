using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Helpers
{
    public static class AESKeyManager
    {
        

        private const string EnvVarName = "REAL_ESTATE_AES_KEY";

        public static string EnsureKeyExists()
        {
            // 🔹 کلید را فقط در سطح کاربر ذخیره کن (بدون نیاز به Admin)
            string existingKey = Environment.GetEnvironmentVariable(EnvVarName, EnvironmentVariableTarget.User);
            if (!string.IsNullOrEmpty(existingKey))
                return existingKey;

            byte[] key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(key);

            string base64Key = Convert.ToBase64String(key);

            Environment.SetEnvironmentVariable(EnvVarName, base64Key, EnvironmentVariableTarget.User);

            return base64Key;
        }
    }
}
