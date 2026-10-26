using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Net.Sockets;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {


        public class NtpTimeFetcher
        {
            public static void GetOfficialTime()
            {
                try
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                    {
                        socket.Connect("ntp.time.ir", 123);

                        // ساخت بسته درخواست NTP
                        byte[] buffer = new byte[48];
                        buffer[0] = 0x1B; // Version 3, Client Mode
                        buffer[1] = 0x00;
                        buffer[2] = 0x00;
                        buffer[3] = 0x00;

                        // ارسال درخواست
                        socket.Send(buffer);

                        // دریافت پاسخ
                        socket.ReceiveTimeout = 3000;
                        int bytesRead = socket.Receive(buffer);

                        if (bytesRead < 48)
                        {
                            Console.WriteLine("پاسخ ناقص دریافت شد.");
                            return;
                        }

                        // استخراج 8 بایت زمان (بایت‌های 40 تا 47)
                        byte[] timeBytes = new byte[8];
                        Array.Copy(buffer, 40, timeBytes, 0, 8);

                        // معکوس کردن بایت‌ها (Big-Endian به Little-Endian)
                        Array.Reverse(timeBytes);

                        // تبدیل به ulong
                        ulong ntpTime = BitConverter.ToUInt64(timeBytes, 0);

                        // تبدیل NTP (شروع از 1900) به Unix (شروع از 1970)
                        // اختلاف: 2208988800 ثانیه
                        // اما چون ntpTime ممکن است بسیار بزرگ باشد، باید با احتیاط عمل کنیم.
                        // ما از روش دستی استفاده می‌کنیم تا از خطای AddSeconds جلوگیری کنیم.

                        // محاسبه ثانیه‌های Unix
                        // اگر ntpTime خیلی بزرگ باشد، ممکن است از محدوده ulong خارج شود، اما در اینجا فرض می‌کنیم درست است.
                        // اما برای اطمینان، از روش دیگری استفاده می‌کنیم:
                        // زمان NTP = (ثانیه‌های از 1900) * 2^32 + (میلی‌ثانیه‌ها)
                        // ما فقط به ثانیه‌ها نیاز داریم.

                        // روش ساده‌تر: استفاده از DateTime و محاسبه اختلاف
                        // اما چون عدد بزرگ است، از روش زیر استفاده می‌کنیم:

                        // محاسبه ثانیه‌های Unix
                        long unixSeconds = (long)(ntpTime / 4294967296L); // تقسیم بر 2^32 برای گرفتن بخش ثانیه
                        long fractionalSeconds = (long)(ntpTime % 4294967296L); // بخش کسری

                        // محاسبه زمان Unix
                        long unixTime = unixSeconds - 2208988800L;

                        // بررسی محدوده
                        if (unixTime < DateTime.MinValue.Ticks / TimeSpan.TicksPerSecond ||
                            unixTime > DateTime.MaxValue.Ticks / TimeSpan.TicksPerSecond)
                        {
                            Console.WriteLine("زمان دریافتی خارج از محدوده معتبر DateTime است.");
                            return;
                        }

                        DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        DateTime utcDateTime = baseDate.AddSeconds(unixTime);

                        Console.WriteLine($"زمان رسمی ایران (میلادی): {utcDateTime:yyyy-MM-dd HH:mm:ss}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"خطا در ارتباط با سرور: {ex.Message}");
                    Console.WriteLine("زمان سیستم: {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            NtpTimeFetcher.GetOfficialTime();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timesystem.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            timeglobal.Text = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");
            difrent.Text = (DateTime.Now.TimeOfDay - DateTime.UtcNow.TimeOfDay).ToString();
        }



        public static DateTime GetNetworkTime()
        {
            const string ntpServer = "pool.ntp.org";
            var ntpData = new byte[48];
            ntpData[0] = 0x1B; // Leap indicator, version, mode

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ntpServer, 123);
                socket.Send(ntpData);
                socket.Receive(ntpData);
            }

            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | ntpData[43];
            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | ntpData[47];

            var milliseconds = (intPart * 1000) + (fractPart * 1000 / 0x100000000L);
            var networkDateTime = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds);

            return networkDateTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetNetworkTime().ToString());
        }
    }
}
