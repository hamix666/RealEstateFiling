
using Common;
using Newtonsoft.Json;

using RealEstateFilingWin.Models;
using RealEstateFilingWin.Models.CUS;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Models.MNG;
using RealEstateFilingWin.Models.Sync;
using RealEstateFilingWin.Services;
using RealEstateFilingWin.Services.Sync;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;


namespace RealEstateFiling.Services
{
    public class RealEstateContext : DbContext
    {
        //public RealEstateContext()
        //   : base(ConfigurationManager.ConnectionStrings["RealEstateDB"].ConnectionString)
        //{
        //    Database.SetInitializer(new RealEstateDbInitializer());
        //}
        public RealEstateContext():base(ClsConnection.Connect())
        {
            Database.SetInitializer(new RealEstateDbInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RealEstateContext, RealEstateFilingWin.Migrations.Configuration>());
            Database.CommandTimeout = 300;
        }
        public RealEstateContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new RealEstateDbInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RealEstateContext, RealEstateFilingWin.Migrations.Configuration>());
            Database.CommandTimeout = 300;

            // حذف فراخوانی ApplyMigrations از اینجا
            try
            {
                if (!Database.CompatibleWithModel(false))
                {
                    Console.WriteLine("Database model is incompatible. Applying migrations...");

                    var configuration = new RealEstateFilingWin.Migrations.Configuration
                    {
                        TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo(connectionString, "System.Data.SqlClient")
                    };

                    var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                    migrator.Update();

                    Console.WriteLine("Migrations applied successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during migration check: {ex.Message}");
            }

        }
        
        private static void ApplyMigrations(string connectionString)
        {
            // پیکربندی Migration که به دیتابیس هدف اشاره می‌کند
            var config = new RealEstateFilingWin.Migrations.Configuration
            {
                // این خط مهم است؛ به Migrator می‌گوید روی کدام دیتابیس کار کند
                TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo(connectionString)
            };

            var migrator = new DbMigrator(config);

            // اگر می‌خواهید همیشه سعی کنید به آخرین نسخه بروید
            migrator.Update();   // یا migrator.Update("نام_آخرین_مهاجرت");
        }
        public virtual DbSet<Owners> Owners_List { get; set; }
        public virtual DbSet<OwnerDet> OwnerDets_List { get; set; }
        public virtual DbSet<FileStatus> FileStatus_List { get; set; }
        public virtual DbSet<FileType> FileType_List { get; set; }
        public virtual DbSet<Region> Region_List { get; set; }
        public virtual DbSet<SanadStatus> SanadStatus_List { get; set; }
        public virtual DbSet<Views> Views_List { get; set; }
        public virtual DbSet<User> Users_List { get; set; }
        public virtual DbSet<AuditLog> AuditLogs_List { get; set; }
        public virtual DbSet<Kabinet> Kabinet_List { get; set; }
        public virtual DbSet<ServiceWc> ServiceWc_List { get; set; }
        public virtual DbSet<Kafposh> Kafposh_List { get; set; }
        public virtual DbSet<Customer> Customer_List { get; set; }
        public virtual DbSet<FileInfo> FileInfo_List { get; set; }
        public virtual DbSet<UserAccsess> UserAccsess_List { get; set; }
        public virtual DbSet<AccsessName> AccsessName_List { get; set; }
        public virtual DbSet<UserOwner> UserOwner_List { get; set; }
        public virtual DbSet<CustomerReadOwner> CustomerReadOwner_List { get; set; }
        public virtual DbSet<UserActivity> UserActivity_List { get; set; }
        public virtual DbSet<UserReminder> UserReminder_List { get; set; }
        public virtual DbSet<OwnersDeleted> OwnersDeleted_List { get; set; }

        public DbSet<Branch> Branches { get; set; }
        public virtual DbSet<RealEstateFilingWin.Models.FIL.SyncState> SyncState_List { get; set; }


        public virtual DbSet<Settings> Settings { get; set; }




        public int CurrentUserId { get; set; } = 0;
        public string CurrentUsername { get; set; } = "System";
        public string CurrentUserIp { get; set; } = GetLocalIPv4();

        public override int SaveChanges()
        {
            var auditLogs = new List<AuditLog>();
            var entries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity is AuditLog)
                    continue;

                string entityName = entry.Entity.GetType().Name;
                string entityId = GetPrimaryKeyValue(entry);

                var log = new AuditLog
                {
                    Username = CurrentUsername,
                    UserID = ClsBaseSetting.UserID.ToInt32OrDefault(),
                    UserIP = CurrentUserIp,
                    EntityName = entityName,
                    EntityId = entityId,
                    ActionTime = DateTime.Now
                };

                switch (entry.State)
                {
                    case EntityState.Added:
                        log.Action = "Insert";
                        log.Changes = JsonConvert.SerializeObject(
                            entry.CurrentValues.PropertyNames.ToDictionary(
                                p => p,
                                p => new { New = entry.CurrentValues[p] }
                            ));
                        break;

                    case EntityState.Modified:
                        log.Action = "Modified";

                        var changes = new List<object>();
                        foreach (var propName in entry.OriginalValues.PropertyNames)
                        {
                            var original = entry.OriginalValues[propName];
                            var current = entry.CurrentValues[propName];
                            if (!Equals(original, current))
                            {
                                changes.Add(new
                                {
                                    Field = propName,
                                    OldValue = original,
                                    NewValue = current
                                });
                            }
                        }

                        // حذف تغییرات تاریخ یا جزئی
                        var significant = changes
                            .Where(c => ((string)c.GetType().GetProperty("Field").GetValue(c)) != "CreateDate"
                                     && ((string)c.GetType().GetProperty("Field").GetValue(c)) != "ModifiDate")
                            .ToList();

                        if (!significant.Any())
                            continue;

                        log.Changes = JsonConvert.SerializeObject(significant);
                        break;

                    case EntityState.Deleted:
                        log.Action = "Delete";
                        log.Changes = JsonConvert.SerializeObject(
                            entry.OriginalValues.PropertyNames.ToDictionary(
                                p => p,
                                p => new { Old = entry.OriginalValues[p] }
                            ));
                        break;
                }

                auditLogs.Add(log);
            }

            // ثبت لاگ‌ها
            if (auditLogs.Any())
                this.AuditLogs_List.AddRange(auditLogs);

            // --- Sync BranchID و LastModified برای ISyncEntity ---
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is ISyncEntity &&
                       (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (ISyncEntity)entry.Entity;

                //if (entry.State == EntityState.Added)
                //    entity.BranchID = ClsBaseSetting.CurrentBranchID;

                entity.LastModified = DateTime.UtcNow;
            }
            
            return base.SaveChanges();
        }

        private string GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager
                .GetObjectStateEntry(entry.Entity);
            if (objectStateEntry.EntityKey.EntityKeyValues != null)
            {
                return objectStateEntry.EntityKey.EntityKeyValues.First().Value?.ToString();
            }
            return "Unknown";
        }

        private static string GetLocalIPv4()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;

            modelBuilder.Entity<Owners>().ToTable("Owners", "FIL");
            modelBuilder.Entity<OwnerDet>().ToTable("OwnerDet", "FIL");
            modelBuilder.Entity<FileInfo>().ToTable("FileInfo", "FIL");
            modelBuilder.Entity<FileStatus>().ToTable("FileStatus", "GNR");
            modelBuilder.Entity<FileType>().ToTable("FileType", "GNR");
            modelBuilder.Entity<Region>().ToTable("Region", "GNR");
            modelBuilder.Entity<FileStatus>().ToTable("FileStatus", "GNR");
            modelBuilder.Entity<SanadStatus>().ToTable("SanadStatus", "GNR");
            modelBuilder.Entity<Views>().ToTable("Views", "GNR");
            modelBuilder.Entity<User>().ToTable("User", "MNG");
            modelBuilder.Entity<AuditLog>().ToTable("AuditLog", "MNG");
            modelBuilder.Entity<Customer>().ToTable("Customer", "CUS");
            modelBuilder.Entity<CustomerReadOwner>().ToTable("CustomerReadOwner", "CUS");
            modelBuilder.Entity<UserAccsess>().ToTable("UserAccsess", "MNG");
            modelBuilder.Entity<AccsessName>().ToTable("AccsessName", "MNG");
            modelBuilder.Entity<UserOwner>().ToTable("UserOwner", "MNG");
            modelBuilder.Entity<UserActivity>().ToTable("UserActivity", "MNG");
            modelBuilder.Entity<Branch>().ToTable("Branch", "MNG");
            modelBuilder.Entity<RealEstateFilingWin.Models.FIL.SyncState>().ToTable("SyncState", "FIL");
            modelBuilder.Entity<Settings>().ToTable("Settings", "GNR");
            modelBuilder.Entity<UserReminder>().ToTable("UserReminder", "MNG");
            modelBuilder.Entity<OwnersDeleted>().ToTable("OwnersDeleted", "FIL");
            

            

            base.OnModelCreating(modelBuilder);
        }

        public static implicit operator EnvironmentVariableTarget(RealEstateContext v)
        {
            throw new NotImplementedException();
        }

    
    }
    
}
