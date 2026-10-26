using Common;
using RealEstateFiling.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Shapes;
using System.Xml.Linq;
using Path = System.IO.Path;
using RealEstateFilingWin.Utility;
using System.Data.Entity;
using RealEstateFilingWin.Models.FIL;
using static DevExpress.Skins.SolidColorHelper;
using System.Diagnostics;
using Janus.Windows.UI.Tab;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmPic : FrmBase
    {
        private readonly Guid melkId;
        private string basePath = Path.Combine(Application.StartupPath, "InfoFile");
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".mp4", ".mov", ".avi", ".mkv" };

        private string melkFolder;


        public FrmPic(Guid id, string MName)
        {
            InitializeComponent();
            melkId = id;
            using (var db = new RealEstateContext())
            {
                var _directory = db.Branches.Where(x => x.ID == ClsBaseSetting.CurrentBranchID).FirstOrDefault();
                var model = db.Users_List.Where(x => x.ID == 1).FirstOrDefault();
                //basePath = Path.Combine(model.Description_Latin, "InfoFile");
                basePath = Path.Combine(_directory.localRoot);
            }

            melkFolder = Path.Combine(basePath, melkId.ToString());

            Directory.CreateDirectory(melkFolder);

            LoadFilesFromDatabase();
            lbltitle.Text = "فایل های مربوط به ___ " + MName;
            lbltitle.AutoSize = true;

        }



        private void lblHint_Click(object sender, EventArgs e)
        {
            OpenFileDialogSelect();
        }

        private void OpenFileDialogSelect()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "عکس و ویدیو|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.mp4;*.mov;*.avi;*.mkv";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    HandleFiles(ofd.FileNames);
                }
            }
        }
        private void HandleFiles(string[] files)
        {
            foreach (var file in files)
            {
                try
                {
                    string ext = Path.GetExtension(file).ToLower();

                    // فقط عکس و ویدیو
                    if (!allowedExtensions.Contains(ext))
                    {
                        FrmMsgBox.Show($"❌ فایل '{Path.GetFileName(file)}' مجاز نیست.", "هشدار", MsgBoxType.Warning);
                        continue;
                    }

                    string fileName = Path.GetFileName(file);
                    string dest = Path.Combine(melkFolder, fileName);

                    // اگر فایل با همین نام وجود دارد
                    if (File.Exists(dest))
                    {
                        var result = FrmMsgBox.Show(
                            $"⚠ فایل '{fileName}' از قبل وجود دارد.\nآیا می‌خواهید جایگزین شود؟",
                            "تأیید جایگزینی", MsgBoxType.Question,
                            MessageBoxButtons.YesNo
                        );

                        if (result == DialogResult.No)
                            continue;

                        // آزادسازی کامل فایل قبل از حذف و جایگزینی
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        // اگر تصویر در کنترل یا حافظه باز مانده، ببندیم
                        try
                        {
                            using (var fs = new FileStream(dest, FileMode.Open, FileAccess.Read))
                            {
                                // فقط برای اطمینان از اینکه فایل در دسترس است
                            }
                        }
                        catch
                        {
                            // اگر فایل قفل بود، صبر کن و GC مجدد اجرا کن
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }

                        File.Delete(dest);
                    }

                    // اطمینان از آزاد بودن هندل‌ها
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    // کپی فایل به مسیر مقصد
                    File.Copy(file, dest, true);

                    // اطلاعات فایل
                    System.IO.FileInfo fi = new System.IO.FileInfo(dest);
                    string mime = GetMimeType(ext);
                    string size = $"{fi.Length / 1024.0:F1} KB";

                    // ذخیره در دیتابیس (می‌تونی داخلش چک کنه اگر وجود داره Update کنه)
                    SaveToDatabase(fileName, dest, mime, size, melkId);

                    // افزودن کارت در UI
                    AddFileCard(dest, fileName, mime, size);

                }
                catch (Exception ex)
                {
                    FrmMsgBox.Show($"⚠ خطا در پردازش فایل '{Path.GetFileName(file)}':\n{ex.Message}", "خطا", MsgBoxType.Error);
                }
            }

        }

        private void SaveToDatabase(string name, string path, string mime, string size, Guid ownerId)
        {
            using (var db = new RealEstateContext())
            {
                var _oldefile = db.FileInfo_List
                    .FirstOrDefault(x => x.OwnerID == ownerId && x.FileName == name);

                string hash = HashHelper.ComputeHash(path);

                if (_oldefile != null)
                {
                    // اگر فایل تغییر کرده باشه
                    if (_oldefile.FileHash != hash)
                    {
                        _oldefile.FileHash = hash;
                        _oldefile.LastModified = DateTime.UtcNow;
                        _oldefile.IsDelete = false;
                        db.SaveChanges();
                    }
                    else if (_oldefile.IsDelete)
                    {
                        _oldefile.LastModified = DateTime.UtcNow;
                        _oldefile.IsDelete = false;
                        db.SaveChanges();
                    }
                    return;
                }

                // فایل جدید
                db.FileInfo_List.Add(new Models.FIL.FileInfo
                {
                    MasterID = Guid.NewGuid(),
                    OwnerID = ownerId,
                    FileName = name,
                    FileHash = hash,
                    LastModified = DateTime.UtcNow,
                    Size = size,
                    MimeType = mime,
                    CreationDate = DateTime.Now,
                    UserIDCreator = ClsBaseSetting.UserID,
                    Description_Latin = melkId.ToString(),
                    FilePath = path,

                });

                db.SaveChanges();
            }
        }
        //private void SaveToDatabase(string name, string path, string mime, string size, Guid ownerId)
        //{
        //    using (var db = new RealEstateContext())
        //    {
        //        var _oldefile=db.FileInfo_List.Where(x => x.FileName == name && x.FilePath == path).FirstOrDefault();
        //        if(_oldefile != null)
        //        {
        //            _oldefile.Size = size;
        //            _oldefile.MimeType=mime;
        //            _oldefile.ModificationDate = DateTime.Now;
        //            _oldefile.UserIDModifier = ClsBaseSetting.UserID;
        //            db.SaveChanges();
        //            return;
        //        }

        //        Models.FIL.FileInfo fileInfo = new Models.FIL.FileInfo();
        //        fileInfo.FileName = name;
        //        fileInfo.FilePath = path;
        //        fileInfo.Size = size;
        //        fileInfo.MimeType = mime;
        //        fileInfo.CreationDate = DateTime.Now;
        //        fileInfo.UserIDCreator = ClsBaseSetting.UserID;
        //        fileInfo.Description_Latin = melkId.ToString();
        //        db.FileInfo_List.Add(fileInfo);
        //        db.SaveChanges();

        //    }
        //}

        private void DeleteFromDatabase(string path)
        {
            using (var db = new RealEstateContext())
            {
                var _file = db.FileInfo_List.Where(x => x.FilePath.Equals(path)).ToList().FirstOrDefault();
                _file.IsDelete = true;
                _file.LastModified = DateTime.UtcNow;
                //db.FileInfo_List.Remove(_file);
                db.SaveChanges();

            }
        }

        private void AddFileCard(string filePath, string fileName, string mime, string size)
        {
            var card = new Panel
            {
                Width = 180,
                Height = 280,
                Margin = new Padding(10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            var pic = new PictureBox
            {
                Width = 160,
                Height = 120,
                Left = 10,
                Top = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand
            };
            var lblType = new Label
            {

                AutoSize = false,
                Width = 160,
                Height = 35,
                Top = 140,
                Left = 10,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };
            // 🔹 تصویر یا آیکن فایل
            if (mime.StartsWith("image"))
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(filePath)))
                {
                    pic.Image = Image.FromStream(ms);
                    lblType.Text = "تصویر";
                }
            }
            else if (mime.StartsWith("video"))
            {
                pic.Image = Properties.Resources.Adobe_Express___file; // آیکن ویدیو
                lblType.Text = "ویدئو";
            }
            //else
            //{
            //    pic.Image = Properties.Resources.file_icon; // آیکن عمومی
            //}

            // 🔹 نام فایل
            var lblName = new Label
            {
                Text = fileName,
                AutoSize = false,
                Width = 160,
                Height = 35,
                Top = 140,
                Left = 10,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black
            };

            // 🔹 اندازه
            var lblSize = new Label
            {
                Text = size,
                AutoSize = false,
                Width = 160,
                Height = 20,
                Top = 175,
                Left = 10,
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // 🔹 دکمه حذف
            var btnDel = new Button
            {
                Text = "🗑 حذف",
                Width = 160,
                Height = 30,
                Left = 10,
                Top = 200,
                BackColor = Color.FromArgb(230, 80, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDel.FlatAppearance.BorderSize = 0;

            // 🔹 دکمه حذف
            

            

            // 🗑 رویداد حذف
            btnDel.Click += (s, e) =>
            {
                if (FrmMsgBox.Show("آیا از حذف این فایل مطمئنید؟", "تأیید حذف", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (pic.Image != null)
                        {
                            pic.Image.Dispose();
                            pic.Image = null;
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        File.Delete(filePath);
                        DeleteFromDatabase(filePath);
                        fileList.Controls.Remove(card);
                    }
                    catch (Exception ex)
                    {
                        FrmMsgBox.Show("⚠ خطا در حذف فایل: " + ex.Message);
                    }
                }
            };

            // 👁 رویداد نمایش فایل (دابل‌کلیک روی کارت یا عکس)
            pic.DoubleClick += (s, e) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    FrmMsgBox.Show("❌ خطا در باز کردن فایل:\n" + ex.Message);
                }
            };


            card.Controls.Add(lblType);
            card.Controls.Add(pic);
            card.Controls.Add(lblName);
            card.Controls.Add(lblSize);
            card.Controls.Add(btnDel);
            
            fileList.Controls.Add(card);
        }


        private string GetMimeType(string ext)
        {
            if (".jpg;.jpeg;.png;.gif;.bmp".Contains(ext)) return "image";
            if (".mp4;.mov;.avi;.mkv".Contains(ext)) return "video";
            return "unknown";
        }



        private void LoadFilesFromDatabase()
        {
            string basePath = "";
            using (var db = new RealEstateContext())
            {
                var model = db.FileInfo_List?.Where(x => x.Description_Latin == melkId.ToString() && x.IsDelete == false).ToList();
                var _directory = db.Branches.Where(x => x.ID == ClsBaseSetting.CurrentBranchID).FirstOrDefault();

                foreach (var item in model)
                {
                    if (File.Exists(item.FilePath))
                        AddFileCard(item.FilePath, item.FileName, item.MimeType, item.Size);
                    else
                    {
                        basePath = Path.Combine(_directory.localRoot, melkId.ToString(), item.FileName);
                        if (File.Exists(basePath))
                            AddFileCard(basePath, item.FileName, item.MimeType, item.Size);
                    }
                }

            }
            if (fileList.Controls.Count == 0)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            HandleFiles(files);
        }

        private void dropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void dropPanel_Click(object sender, EventArgs e)
        {
            OpenFileDialogSelect();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmPic_Load(object sender, EventArgs e)
        {

        }
    }
}
