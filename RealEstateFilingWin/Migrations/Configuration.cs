namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RealEstateFiling.Services.RealEstateContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RealEstateFiling.Services.RealEstateContext context)
        {
            if (!context.FileType_List.Any())
            {
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "آپارتمان" });
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "اداری" });
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "مستقلات" });
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "مغازه" });
            }
            if (!context.Region_List.Any())
            {
                context.Region_List.Add(new Models.GNR.Region() { Name = "نیرو هوایی" });
                context.Region_List.Add(new Models.GNR.Region() { Name = "تهران نو" });
                context.Region_List.Add(new Models.GNR.Region() { Name = "دهقان" });
                context.Region_List.Add(new Models.GNR.Region() { Name = "پرستار" });

            }
            if (!context.FileStatus_List.Any())
            {
                context.FileStatus_List.Add(new Models.GNR.FileStatus() { Name = "مالک" });
                context.FileStatus_List.Add(new Models.GNR.FileStatus() { Name = "مستاجر" });
                context.FileStatus_List.Add(new Models.GNR.FileStatus() { Name = "تخلیه" });


            }
            if (!context.Views_List.Any())
            {
                context.Views_List.Add(new Models.GNR.Views() { Name = "سنگ" });
                context.Views_List.Add(new Models.GNR.Views() { Name = "آجر" });
                context.Views_List.Add(new Models.GNR.Views() { Name = "مدرن" });
                context.Views_List.Add(new Models.GNR.Views() { Name = "کلاسیک" });


            }
            if (!context.SanadStatus_List.Any())
            {
                context.SanadStatus_List.Add(new Models.GNR.SanadStatus() { Name = "آماده" });
                context.SanadStatus_List.Add(new Models.GNR.SanadStatus() { Name = "غیر آماده" });



            }
            if (!context.Users_List.Any())
            {
                context.Users_List.Add(new Models.MNG.User() { Name="مدیر اصلی",Username = "admin", Password = "$2a$11$yEl19UuO2qVi1McIYBLfzeELEX3.Wt1n2A/TiPfnpeypnY/YDT922", UserIDCreator = 1,CreationDate=DateTime.Now });
            }
            if (!context.Kafposh_List.Any())
            {
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "سنگ" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "سرامیک" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "اسلپ" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "پارکت" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "لمینت" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "موکت" });
            }
            if (!context.Kabinet_List.Any())
            {
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "MDF" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "هایگلاس" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "ممبران" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "نئوکلاسیک" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "انزو" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "فلزی" });
            }
            if (!context.ServiceWc_List.Any())
            {
                context.ServiceWc_List.Add(new Models.GNR.ServiceWc() { Name = "ایرانی و فرنگی" });
                context.ServiceWc_List.Add(new Models.GNR.ServiceWc() { Name = "ایرانی" });
                context.ServiceWc_List.Add(new Models.GNR.ServiceWc() { Name = "فرنگی" });
            }
            if (!context.AccsessName_List.Any())
            {
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 1, Name = "فروش", Name_E = "Forosh" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 2, Name = "پیش فروش", Name_E = "PishForosh" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 3, Name = "رهن و اجاره", Name_E = "Ejare" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 4, Name = "آپارتمان", Name_E = "Aparteman",value=1 });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 5, Name = "اداری", Name_E = "Edari",value=2 });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 6, Name = "مستقلات", Name_E = "Mostaghelat",value=3 });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 7, Name = "مغازه", Name_E = "Maghaze",value=4 });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 8, Name = "ویرایش ملک", Name_E = "EditMelk" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 9, Name = "احیای فایل", Name_E = "EhyaeFile" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 10, Name = "پوشه شخصی", Name_E = "PosheShakhsi" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 11, Name = "لیست ذخیره جستجو", Name_E = "SaveSearch" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 12, Name = "ایجاد ملک", Name_E = "NewMelk" });
                context.AccsessName_List.Add(new Models.MNG.AccsessName() { ID = 13, Name = "مدیریت کاربران", Name_E = "UserManage" });
            }
            if (!context.Settings.Any())
            {
                context.Settings.Add(new Models.GNR.Settings() { ID=Guid.NewGuid(),Active=false});
            }
            context.SaveChanges();
            try
            {
                context.Database.ExecuteSqlCommand(
                    "ALTER DATABASE [" + context.Database.Connection.Database + "] COLLATE Persian_100_CI_AS;"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Collation update failed: " + ex.Message);
            }
        }
    }
}
