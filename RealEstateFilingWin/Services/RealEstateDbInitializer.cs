using RealEstateFiling.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace RealEstateFilingWin.Services
{
    public class RealEstateDbInitializer : CreateDatabaseIfNotExists<RealEstateContext>
    {
        protected override void Seed(RealEstateContext context)
        {
            if (!context.FileType_List.AsNoTracking().Any())
            {
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "آپارتمان" });
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "اداری" });
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "مستقلات" });
                context.FileType_List.Add(new Models.GNR.FileType() { Name = "مغازه" });
            }
            if (!context.Region_List.AsNoTracking().Any())
            {
                context.Region_List.Add(new Models.GNR.Region() { Name = "نیرو هوایی" });
                context.Region_List.Add(new Models.GNR.Region() { Name = "تهران نو" });
                context.Region_List.Add(new Models.GNR.Region() { Name = "دهقان" });
                context.Region_List.Add(new Models.GNR.Region() { Name = "پرستار" });

            }
            if (!context.FileStatus_List.AsNoTracking().Any())
            {
                context.FileStatus_List.Add(new Models.GNR.FileStatus() { Name = "مالک" });
                context.FileStatus_List.Add(new Models.GNR.FileStatus() { Name = "مستاجر" });
                context.FileStatus_List.Add(new Models.GNR.FileStatus() { Name = "تخلیه" });


            }
            if (!context.Views_List.AsNoTracking().Any())
            {
                context.Views_List.Add(new Models.GNR.Views() { Name = "سنگ" });
                context.Views_List.Add(new Models.GNR.Views() { Name = "آجر" });
                context.Views_List.Add(new Models.GNR.Views() { Name = "مدرن" });
                context.Views_List.Add(new Models.GNR.Views() { Name = "کلاسیک" });


            }
            if (!context.SanadStatus_List.AsNoTracking().Any())
            {
                context.SanadStatus_List.Add(new Models.GNR.SanadStatus() { Name = "آماده" });
                context.SanadStatus_List.Add(new Models.GNR.SanadStatus() { Name = "غیر آماده" });



            }
            if (!context.Users_List.AsNoTracking().Any())
            {
                context.Users_List.Add(new Models.MNG.User() { Name = "مدیر اصلی", Username = "admin", Password = "$2a$11$yEl19UuO2qVi1McIYBLfzeELEX3.Wt1n2A/TiPfnpeypnY/YDT922", UserIDCreator = 1, CreationDate = DateTime.Now });
            }
            if (!context.Kafposh_List.AsNoTracking().Any())
            {
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "سنگ" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "سرامیک" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "اسلپ" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "پارکت" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "لمینت" });
                context.Kafposh_List.Add(new Models.GNR.Kafposh() { Name = "موکت" });
            }
            if (!context.Kabinet_List.AsNoTracking().Any())
            {
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "MDF" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "هایگلاس" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "ممبران" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "نئوکلاسیک" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "انزو" });
                context.Kabinet_List.Add(new Models.GNR.Kabinet() { Name = "فلزی" });
            }
            if (!context.ServiceWc_List.AsNoTracking().Any())
            {
                context.ServiceWc_List.Add(new Models.GNR.ServiceWc() { Name = "ایرانی و فرنگی" });
                context.ServiceWc_List.Add(new Models.GNR.ServiceWc() { Name = "ایرانی" });
                context.ServiceWc_List.Add(new Models.GNR.ServiceWc() { Name = "فرنگی" });
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
            base.Seed(context);
        }
    }
}
