using System;

    using System.IO;
    using System.Security.Cryptography;

namespace RealEstateFilingWin.Utility
{
    

    public static class HashHelper
    {
        public static string ComputeHash(string filePath)
        {
            using (var sha = SHA256.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = sha.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
