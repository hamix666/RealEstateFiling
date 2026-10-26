using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Helpers
{
    public static class PasswordStore
    {
        private static string FilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RealEstateFilingWin",
            "sqlpass.dat");

        public static void SaveEncryptedPackage(string base64Package)
        {
            var dir = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(FilePath, base64Package);
        }

        public static string LoadEncryptedPackage()
        {
            if (!File.Exists(FilePath)) return null;
            return File.ReadAllText(FilePath);
        }
    }
}
