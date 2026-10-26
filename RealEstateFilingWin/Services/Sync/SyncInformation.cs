using Newtonsoft.Json.Linq;
using RealEstateFiling.Services;
using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Models.MNG;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Services.Sync
{
    public class SyncInformation
    {
        private static Timer _timer;
        private static readonly RealEstateContext _context = new RealEstateContext();
        private static readonly Settings setting = _context.Settings.FirstOrDefault();
        public static void Start()
        {
            _timer = new Timer(Sync, null, TimeSpan.Zero, TimeSpan.FromMinutes(setting.IntervalInfoUpdate));
        }

        public static void Sync(object state)
        {
            try
            {
                AppLogger.Log("=== Sync Started Information ===");

                using (var local = new RealEstateContext())
                using (var master = new RealEstateContext(setting.masterConnectionString))
                {
                    var mastersetting = master.Settings.FirstOrDefault();











                    //<---------------Update---------------->

                    local.Settings.AddOrUpdate(mastersetting);
                    var masteruser = master.Users_List.AsNoTracking().ToList();
                    var localuser = local.Users_List.AsNoTracking().ToList();
                    SyncData<Models.MNG.User>(localuser, masteruser, x => x.ID, local);

                    var masterUserAccsess = master.UserAccsess_List.AsNoTracking().ToList();
                    var localUserAccsess = local.UserAccsess_List.AsNoTracking().ToList();
                    SyncData<Models.MNG.UserAccsess>(localUserAccsess, masterUserAccsess, x => x.ID, local);

                    var masterKabinet = master.Kabinet_List.AsNoTracking().ToList();
                    var localKabinet = local.Kabinet_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.Kabinet>(localKabinet, masterKabinet, x => x.ID, local);

                    var masterKafposh = master.Kafposh_List.AsNoTracking().ToList();
                    var localKafposh = local.Kafposh_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.Kafposh>(localKafposh, masterKafposh, x => x.ID, local);


                    var masterServiceWc = master.ServiceWc_List.AsNoTracking().ToList();
                    var localServiceWc = local.ServiceWc_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.ServiceWc>(localServiceWc, masterServiceWc, x => x.ID, local);


                    var masterRegion = master.Region_List.AsNoTracking().ToList();
                    var localRegion = local.Region_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.Region>(localRegion, masterRegion, x => x.ID, local);


                    var masterFileType = master.FileType_List.AsNoTracking().ToList();
                    var localFileType = local.FileType_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.FileType>(localFileType, masterFileType, x => x.ID, local);


                    var masterFileStatus = master.FileStatus_List.AsNoTracking().ToList();
                    var localFileStatus = local.FileStatus_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.FileStatus>(localFileStatus, masterFileStatus, x => x.ID, local);


                    var masterSanadStatus = master.SanadStatus_List.AsNoTracking().ToList();
                    var localSanadStatus = local.SanadStatus_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.SanadStatus>(localSanadStatus, masterSanadStatus, x => x.ID, local);


                    var masterViews = master.Views_List.AsNoTracking().ToList();
                    var localViews = local.Views_List.AsNoTracking().ToList();
                    SyncData<Models.GNR.Views>(localViews, masterViews, x => x.ID, local);


                    var masterBranches = master.Branches.AsNoTracking().ToList();
                    var localBranches = local.Branches.AsNoTracking().ToList();
                    SyncData<Models.MNG.Branch>(localBranches, masterBranches, x => x.ID, local);





                    AppLogger.Log("Saving Information Local DB");
                    local.SaveChanges();
                }

                AppLogger.Log("=== Sync Information Finished Successfully ===");
            }
            catch (Exception ex)
            {
                AppLogger.Log("!!! Sync Information FAILED !!!");
                AppLogger.Log(ex.ToString());
            }
        }

        public static void SyncData<T>(List<T> localList, List<T> masterList, Func<T, object> idSelector, RealEstateContext local)
        {
            var localDict = localList.ToDictionary(x => idSelector(x));

            foreach (var masterItem in masterList)
            {
                var localItem = localDict.FirstOrDefault(x => x.Key.Equals(idSelector(masterItem))).Value;

                if (localItem == null)
                {
                    // Add new item
                    localList.Add(masterItem);
                    local.Entry(masterItem).State = EntityState.Added; // Explicitly set state
                }
                else
                {
                    localItem = masterItem;

                    local.Entry(localItem).State = EntityState.Modified; // Explicitly set state
                }
            }
            // Remove records from localList that are not in masterList
            var keysToRemove = localDict.Keys.Except(masterList.Select(x => idSelector(x))).ToList();
            foreach (var key in keysToRemove)
            {
                var itemToRemove = localList.FirstOrDefault(x => idSelector(x).Equals(key));
                if (itemToRemove != null)
                {
                    localList.Remove(itemToRemove);
                    local.Entry(itemToRemove).State = EntityState.Deleted;
                }
            }
        }




    }
}
