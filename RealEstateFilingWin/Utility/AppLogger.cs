using System;
using System.Globalization;
using System.IO;
using System.Text;

public static class AppLogger
{
    private static readonly object _lock = new object();
    private static readonly string LogPath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

    public static void Log(string message)
    {
        try
        {
            lock (_lock)
            {
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);

                string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
                string fullPath = Path.Combine(LogPath, fileName);

                File.AppendAllText(fullPath,
                    $"{GetShamsiNow()} | {message}{Environment.NewLine}",
                    Encoding.UTF8);
            }
        }
        catch
        {
            // اگر لاگ هم خطا داد دیگر واقعاً سرنوشت قبول کرده شکست بخوری
        }
    }

    private static string GetShamsiNow()
    {
        var pc = new PersianCalendar();
        var now = DateTime.Now;

        return $"{pc.GetYear(now)}/{pc.GetMonth(now):00}/{pc.GetDayOfMonth(now):00} " +
               $"{pc.GetHour(now):00}:{pc.GetMinute(now):00}:{pc.GetSecond(now):00}";
    }
}