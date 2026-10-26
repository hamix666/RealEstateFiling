



using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using Common;
using FluentFTP;
using RealEstateFiling.Services;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Models.MNG;

namespace RealEstateFilingWin.Services.Sync
{
    public class FileSyncService
    {
        private static Timer _timer;
        private static readonly RealEstateContext _context = new RealEstateContext();
        private static readonly Settings setting = _context.Settings.FirstOrDefault();
        private static readonly Guid _brancid = Guid.Parse(RealEstateFilingWin.Properties.Settings.Default.branchid == "" ? "00000000-0000-0000-0000-000000000000" : RealEstateFilingWin.Properties.Settings.Default.branchid);
        private static readonly Branch branch = _context.Branches.Where(x => x.ID == _brancid).FirstOrDefault();
        // تنظیمات FTP با FluentFTP
        private static readonly string ftpServer = setting?.ftpServer; // "menubaan.ir";
        private static readonly int ftpPort = (int)(setting?.ftpPort); // 21;
        private static readonly string ftpUser = setting?.ftpUser; // "hamid2";
        private static readonly string ftpPass = setting?.ftpPass; // "H@mid4850";
        private static readonly string ftpRootPath = setting?.ftpRootPath; // "/Owners"; // مسیر اصلی روی سرور

        private static readonly string localRoot = @"" + branch?.localRoot + "";
        private static readonly string logFile = @"sync_log.txt";

        // کانکشن استرینگ دیتابیس مستر (روی سرور)
        private static readonly string masterConnectionString = setting.masterConnectionString;// "Server=menubaan.ir; Database=softenia_amlak;user id=softenia_amlak; password=H@mid4850; Trusted_Connection=False; MultipleActiveResultSets=true;Encrypt=False";

        public static void Start()
        {
            try
            {
                Log("FileSyncService started...");

                // تست اولیه اتصال FTP
                if (TestFtpConnection())
                {
                    _timer = new Timer(Sync, null, TimeSpan.Zero, TimeSpan.FromMinutes(setting.IntervalFileUpdate));
                    Log("Timer set to 5 minutes interval");
                }
                else
                {
                    Log("FTP connection test failed. Service will not start.");
                }
            }
            catch (Exception ex)
            {
                Log($"Error in Start: {ex.Message}");
            }
        }

        private static bool TestFtpConnection()
        {
            try
            {
                Log($"Testing FTP connection to {ftpServer}:{ftpPort}...");

                using (var client = new FtpClient(ftpServer, ftpUser, ftpPass, ftpPort))
                {
                    client.ValidateCertificate += (control, e) => e.Accept = true;
                    client.Connect();

                    if (client.IsConnected)
                    {
                        Log($"FTP connection successful!");

                        // بررسی وجود مسیر Owners
                        if (client.DirectoryExists(ftpRootPath))
                        {
                            var listings = client.GetListing(ftpRootPath);
                            Log($"Found {listings.Length} items in {ftpRootPath}");
                        }
                        else
                        {
                            Log($"Warning: {ftpRootPath} does not exist on server");
                            client.CreateDirectory(ftpRootPath);
                            Log($"Created directory {ftpRootPath}");
                        }

                        client.Disconnect();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"FTP connection failed: {ex.Message}");
                return false;
            }

            return false;
        }

        private static void Sync(object state)
        {
            Log("========== Sync started ==========");

            try
            {
                // بررسی وجود پوشه محلی
                if (!Directory.Exists(localRoot))
                {
                    Log($"Local root not found: {localRoot}");
                    return;
                }

                // اتصال به دو دیتابیس
                using (var localDb = new RealEstateContext())
                using (var masterDb = new RealEstateContext(masterConnectionString))
                {
                    // دریافت وضعیت آخرین همگام‌سازی
                    var syncState = localDb.SyncState_List.FirstOrDefault();
                    if (syncState == null)
                    {
                        // اگر رکوردی وجود نداشت، یک رکورد جدید ایجاد کن
                        syncState = new Models.FIL.SyncState
                        {
                            LastPushImage = DateTime.UtcNow.AddDays(-1), // یک روز قبل
                            LastPullImage = DateTime.UtcNow.AddDays(-1)
                        };
                        localDb.SyncState_List.Add(syncState);
                        localDb.SaveChanges();
                        Log("Created new sync state record");
                    }

                    // ========== مرحله 1: ارسال فایل‌های جدید از لوکال به مستر ==========
                    Log("=== PUSH: Local to Master ===");

                    // فایل‌هایی که بعد از آخرین ارسال تغییر کرده‌اند
                    var filesToPush = localDb.FileInfo_List
                        .Where(x => x.LastModified > syncState.LastPushImage)
                        .ToList();

                    Log($"Found {filesToPush.Count} files to push to master");
                    if (filesToPush != null && filesToPush.Count > 0)
                    {
                        foreach (var fileInfo in filesToPush)
                        {
                            try
                            {
                                string ownerFolder = Path.Combine(localRoot, fileInfo.OwnerID.ToString());
                                string localFilePath = Path.Combine(ownerFolder, fileInfo.FileName);

                                if (!File.Exists(localFilePath))
                                {
                                    Log($"Local file not found: {localFilePath}");
                                    //continue;
                                }

                                Log($"Pushing file: {fileInfo.FileName} for owner: {fileInfo.OwnerID}");

                                // آپلود فایل به FTP
                                if (UploadFile(localFilePath, fileInfo.OwnerID, fileInfo.IsDelete))
                                {

                                    // محاسبه مجدد هش (برای اطمینان)
                                    //string currentHash = ComputeHash(localFilePath);

                                    // بررسی وجود فایل در دیتابیس مستر
                                    var masterFile = masterDb.FileInfo_List
                                        .FirstOrDefault(x => x.OwnerID == fileInfo.OwnerID &&
                                                            x.FileName == fileInfo.FileName);

                                    if (masterFile == null)
                                    {
                                        // اضافه کردن به دیتابیس مستر
                                        masterDb.FileInfo_List.Add(new RealEstateFilingWin.Models.FIL.FileInfo
                                        {
                                            FileName = fileInfo.FileName,
                                            FilePath = $"{ftpRootPath}/{fileInfo.OwnerID}/{fileInfo.FileName}",
                                            FileHash = fileInfo.FileHash,
                                            OwnerID = fileInfo.OwnerID,
                                            MasterID = Guid.NewGuid(),
                                            LastModified = fileInfo.LastModified,
                                            CreationDate = fileInfo.CreationDate,
                                            MimeType = fileInfo.MimeType,
                                            Size = fileInfo.Size,
                                            UserIDModifier = fileInfo.UserIDModifier,
                                            ModificationDate = fileInfo.ModificationDate,
                                            UserIDCreator = fileInfo.UserIDCreator,
                                            Description = fileInfo.Description,
                                            Description_Latin = fileInfo.Description_Latin,
                                            IsDelete = fileInfo.IsDelete
                                        });
                                        Log($"Added file record to master database: {fileInfo.FileName}");
                                    }
                                    else
                                    {

                                        // به‌روزرسانی در دیتابیس مستر
                                        masterFile.FileHash = fileInfo.FileHash;
                                        masterFile.LastModified = fileInfo.LastModified;
                                        masterFile.IsDelete = fileInfo.IsDelete;
                                        Log($"Updated file record in master database: {fileInfo.FileName}");
                                    }

                                    // به‌روزرسانی LastModified در دیتابیس لوکال (برای جلوگیری از ارسال مجدد)
                                    //fileInfo.LastModified = DateTime.UtcNow;
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"Error pushing file {fileInfo.FileName}: {ex.Message}");
                                return;
                            }
                        }

                        // ذخیره تغییرات در دیتابیس مستر
                        masterDb.SaveChanges();

                        // به‌روزرسانی زمان آخرین ارسال
                        syncState.LastPushImage = DateTime.UtcNow;
                        localDb.SaveChanges();
                        Log($"Push completed. LastPushImage updated to: {syncState.LastPushImage}");
                    }
                    // ========== مرحله 2: دریافت فایل‌های جدید از مستر به لوکال ==========
                    Log("=== PULL: Master to Local ===");

                    // فایل‌هایی که بعد از آخرین دریافت در مستر تغییر کرده‌اند
                    var filesToPull = masterDb.FileInfo_List
                        .Where(x => x.LastModified > syncState.LastPullImage)
                        .ToList();

                    Log($"Found {filesToPull.Count} files to pull from master");
                    if (filesToPull != null && filesToPull.Count > 0)
                    {
                        foreach (var masterFile in filesToPull)
                        {
                            try
                            {
                                string ownerFolder = Path.Combine(localRoot, masterFile.OwnerID.ToString());
                                Directory.CreateDirectory(ownerFolder);

                                string localFilePath = Path.Combine(ownerFolder, masterFile.FileName);

                                Log($"Pulling file: {masterFile.FileName} for owner: {masterFile.OwnerID}");

                                // دانلود فایل از FTP
                                if (DownloadFile(masterFile.OwnerID, masterFile.FileName, ownerFolder, masterFile.IsDelete))
                                {
                                    // محاسبه هش فایل دانلود شده
                                    string downloadedFileHash = "";


                                    // بررسی وجود فایل در دیتابیس لوکال
                                    var localFile = localDb.FileInfo_List
                                        .FirstOrDefault(x => x.OwnerID == masterFile.OwnerID &&
                                                            x.FileName == masterFile.FileName);

                                    if (masterFile.IsDelete)
                                        downloadedFileHash = localFile.FileHash;
                                    else
                                        downloadedFileHash = ComputeHash(localFilePath);

                                    if (localFile == null)
                                    {
                                        // اضافه کردن به دیتابیس لوکال
                                        localDb.FileInfo_List.Add(new RealEstateFilingWin.Models.FIL.FileInfo
                                        {
                                            FileName = masterFile.FileName,
                                            FilePath = localFilePath,
                                            FileHash = downloadedFileHash,
                                            OwnerID = masterFile.OwnerID,
                                            MasterID = Guid.NewGuid(),
                                            LastModified = masterFile.LastModified,
                                            CreationDate = masterFile.CreationDate,
                                            Description = masterFile.Description,
                                            Description_Latin = masterFile.Description_Latin,
                                            MimeType = masterFile.MimeType,
                                            ModificationDate = masterFile.ModificationDate,
                                            UserIDCreator = masterFile.UserIDCreator,
                                            UserIDModifier = masterFile.UserIDModifier,
                                            Size = masterFile.Size,
                                            IsDelete = masterFile.IsDelete,
                                        });
                                        Log($"Added file record to local database: {masterFile.FileName}");
                                    }
                                    else
                                    {
                                        // به‌روزرسانی در دیتابیس لوکال
                                        localFile.FileHash = downloadedFileHash;
                                        localFile.LastModified = masterFile.LastModified;
                                        localFile.IsDelete = masterFile.IsDelete;
                                        Log($"Updated file record in local database: {masterFile.FileName}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"Error pulling file {masterFile.FileName}: {ex.Message}");
                                return;
                            }
                        }

                        // ذخیره تغییرات در دیتابیس لوکال
                        localDb.SaveChanges();

                        // به‌روزرسانی زمان آخرین دریافت
                        syncState.LastPullImage = DateTime.UtcNow;
                        localDb.SaveChanges();
                        Log($"Pull completed. LastPullImage updated to: {syncState.LastPullImage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Critical error in Sync: {ex}");
            }

            Log("========== Sync finished ==========");
        }

        private static bool UploadFile(string localPath, Guid ownerId, bool _isdelete)
        {
            string fileName = Path.GetFileName(localPath);
            string remoteDir = $"{ftpRootPath}/{ownerId}";
            string remotePath = $"{remoteDir}/{fileName}";

            try
            {
                Log($"Uploading {fileName} to {remotePath}...");

                using (var client = new FtpClient(ftpServer, ftpUser, ftpPass, ftpPort))
                {
                    client.ValidateCertificate += (control, e) => e.Accept = true;
                    client.Connect();
                    if (_isdelete)
                    {
                        if (client.FileExists(remotePath))
                        {
                            client.DeleteFile(remotePath);
                            Log($"Delete file is dlete {fileName}");

                        }
                        client.Disconnect();
                        return true;
                    }
                    // اطمینان از وجود پوشه مالک
                    if (!client.DirectoryExists(remoteDir))
                    {
                        client.CreateDirectory(remoteDir);
                        Log($"Created directory {remoteDir}");
                    }

                    // آپلود فایل
                    var status = client.UploadFile(localPath, remotePath, FtpRemoteExists.Overwrite, true);

                    if (status == FtpStatus.Success)
                    {
                        Log($"Uploaded {fileName} successfully");
                        client.Disconnect();
                        return true;
                    }
                    else
                    {
                        Log($"Upload failed with status: {status}");
                        client.Disconnect();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Upload failed for {fileName}: {ex.Message}");
                return false;
            }
        }

        private static bool DownloadFile(Guid ownerId, string fileName, string localFolder, bool _isdelete)
        {
            string remotePath = $"{ftpRootPath}/{ownerId}/{fileName}";
            string localPath = Path.Combine(localFolder, fileName);

            try
            {
                Log($"Downloading {fileName} from {remotePath}...");
                if (_isdelete)
                {
                    File.Delete(localPath);
                    return true;
                }
                using (var client = new FtpClient(ftpServer, ftpUser, ftpPass, ftpPort))
                {
                    client.ValidateCertificate += (control, e) => e.Accept = true;
                    client.Connect();

                    // دانلود فایل
                    var status = client.DownloadFile(localPath, remotePath, FtpLocalExists.Overwrite, FtpVerify.Retry);

                    if (status == FtpStatus.Success)
                    {
                        Log($"Downloaded {fileName} successfully");
                        client.Disconnect();
                        return true;
                    }
                    else
                    {
                        Log($"Download failed with status: {status}");
                        client.Disconnect();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Download failed for {fileName}: {ex.Message}");
                return false;
            }
        }

        private static string ComputeHash(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(stream);
                return Convert.ToBase64String(hash);
            }
        }

        private static void Log(string message)
        {
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\r\n";
                File.AppendAllText(logFile, logMessage);
                System.Diagnostics.Debug.WriteLine(logMessage);
            }
            catch
            {
                // اگر لاگ نوشتن هم مشکل داشت، نادیده بگیر
            }
        }
    }

    // مدل SyncState (اگر وجود ندارد)
    public class SyncState
    {
        public Guid Id { get; set; }
        public DateTime LastPushImage { get; set; }
        public DateTime LastPullImage { get; set; }
    }
}