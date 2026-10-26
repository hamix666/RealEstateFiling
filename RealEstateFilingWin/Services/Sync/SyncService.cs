

using DevExpress.XtraBars.Docking;
using RealEstateFiling.Services;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Models.GNR;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RealEstateFilingWin.Services.Sync
{
    public class SyncService
    {
        private static  System.Threading.Timer _timer;
        private static readonly RealEstateContext _context = new RealEstateContext();
        private static readonly Settings setting = _context.Settings.FirstOrDefault();
        public static void Start()
        {
            _timer = new System.Threading.Timer(Sync, null, TimeSpan.Zero, TimeSpan.FromMinutes(setting.IntervalOwnerUpdate));
        }
        private static int count_changedLocal_owner = 0;
        private static int count_changedLocal_ownerdet = 0;
        private static int count_changedMaster_owner = 0;
        private static int count_changedMaster_ownerdet = 0;

        private static int count_push = 0;
        private static int count_pull = 0;
        private static void Sync(object state)
        {
            try
            {
                AppLogger.Log("=== Sync Started ===");
                using (var local = new RealEstateContext())
                using (var master = new RealEstateContext(setting.masterConnectionString))
                {
                    var syncState = local.SyncState_List.FirstOrDefault();

                    DateTime lastPush = syncState.LastPush ?? DateTime.MinValue;
                    DateTime lastPull = syncState.LastPull ?? DateTime.MinValue;
                    DateTime LastDeleteOwner = syncState.LastDeleteOwner ?? DateTime.MinValue;

                    AppLogger.Log($"LastPush = {lastPush}");
                    AppLogger.Log($"LastPull = {lastPull}");

                    AppLogger.Log("PushOwners started");
                    PushOwners(local, master, lastPush);


                    AppLogger.Log("Saving PushOwners Master DB");
                    master.SaveChanges();

                    AppLogger.Log("PushOwnerDet started");
                    PushOwnerDet(local, master, lastPush);

                    AppLogger.Log("Saving PushOwnerDet Master DB");
                    master.SaveChanges();

                    AppLogger.Log("PullOwners started");
                    PullOwners(local, master, lastPull);

                    AppLogger.Log("Saving PullOwners Local DB");
                    local.SaveChanges();

                    AppLogger.Log("PullOwnerDet started");
                    PullOwnerDet(local, master, lastPull);
                    AppLogger.Log("Saving PullOwnerDet Local DB");
                    local.SaveChanges();

                    if (count_changedLocal_owner > 0 || count_changedLocal_ownerdet>0)
                    {
                        syncState.LastPush = DateTime.UtcNow;
                    }
                    if (count_changedMaster_owner > 0 || count_changedMaster_ownerdet>0)
                    {
                        syncState.LastPull = DateTime.UtcNow;
                    }


                    var _deleteownermaster = master.OwnersDeleted_List.Where(x=>x.LastModified>LastDeleteOwner).ToList();
                    if (_deleteownermaster != null && _deleteownermaster.Count>0)
                    {
                        var _deleteowneridmaster = _deleteownermaster.Select(x => x.OwnerGuiD).ToList();
                        if (_deleteowneridmaster != null)
                        {
                            var _localowner = local.Owners_List.Where(x => _deleteowneridmaster.Contains(x.MasterID)).ToList();
                            if (_localowner != null)
                            {
                                
                                local.Owners_List.RemoveRange(_localowner);
                                syncState.LastDeleteOwner = DateTime.UtcNow;

                            }

                        }
                    }



                    AppLogger.Log("Saving PullOwnerDet Local DB");
                    local.SaveChanges();
                }

                AppLogger.Log("=== Sync Finished Successfully ===");
            }
            catch (Exception ex)
            {
                AppLogger.Log("!!! Sync FAILED !!!");
                AppLogger.Log(ex.ToString());
            }
        }

        private static void PushOwners(RealEstateContext local, RealEstateContext master, DateTime lastPush)
        {
            var changedLocal = local.Owners_List
                .Where(x => x.LastModified > lastPush)
                .ToList();
            count_changedLocal_owner = changedLocal.Count();
            AppLogger.Log("count_changedLocal_owner = "+ count_changedLocal_owner);
            foreach (var item in changedLocal)
            {
                if (item.MasterID == Guid.Empty)
                    item.MasterID = Guid.NewGuid();

                var masterRow = master.Owners_List
                    .FirstOrDefault(x => x.MasterID == item.MasterID);

                if (masterRow == null)
                {
                    master.Owners_List.Add(new Owners
                    {
                        MasterID = item.MasterID,
                        MName = item.MName,
                        LastModified = item.LastModified,
                        Address = item.Address,
                        AllTabaghe = item.AllTabaghe,
                        AllVahed = item.AllVahed,
                        BranchID = item.BranchID,
                        Code = item.Code,
                        CoolA = item.CoolA,
                        CoolB = item.CoolB,
                        CreationDate = item.CreationDate,
                        DatePishForoshTahvil = item.DatePishForoshTahvil,
                        Description = item.Description,
                        Description_Latin = item.Description_Latin,
                        Det = item.Det,
                        Ejare = item.Ejare,
                        Ejtemat = item.Ejtemat,
                        Eslahi = item.Eslahi,
                        Estakhr = item.Estakhr,
                        FileStatusID = item.FileStatusID,
                        FiletypeID = item.FiletypeID,
                        Forosh = item.Forosh,
                        Gharbi = item.Gharbi,
                        Hayat = item.Hayat,
                        IsDelete = item.IsDelete,
                        IsDeleteDateTime = item.IsDeleteDateTime,
                        Jakozi = item.Jakozi,
                        Jaroo = item.Jaroo,
                        Jonobi = item.Jonobi,
                        MapLat = item.MapLat,
                        MapLong = item.MapLong,
                        Masahat = item.Masahat,
                        ModificationDate = item.ModificationDate,
                        MTel1 = item.MTel1,
                        MTel2 = item.MTel2,
                        MTel3 = item.MTel3,
                        Pakage = item.Pakage,
                        Pasio = item.Pasio,
                        PishForosh = item.PishForosh,
                        Plak = item.Plak,
                        PMotor = item.PMotor,
                        RegionID = item.RegionID,
                        ReturnTime = item.ReturnTime,
                        RoofGarden = item.RoofGarden,
                        RowVersion = item.RowVersion,
                        SanadStatusID = item.SanadStatusID,
                        Senbana = item.Senbana,
                        Sharghi = item.Sharghi,
                        Shofazh = item.Shofazh,
                        Shomali = item.Shomali,
                        SName = item.SName,
                        Sona = item.Sona,
                        Spilit = item.Spilit,
                        STel1 = item.STel1,
                        STel2 = item.STel2,
                        STel3 = item.STel3,
                        Tajmi = item.Tajmi,
                        Tol = item.Tol,
                        Vahed = item.Vahed,
                        ViewID = item.ViewID,
                        Warmer = item.Warmer,
                        UserIDCreator = item.UserIDCreator,
                        UserIDModifier = item.UserIDModifier,
                        ViewPhoneDate = item.ViewPhoneDate,
                        ViewPhoneUserID = item.ViewPhoneUserID,
                        





                    });

                    count_push++;
                }
                else if (masterRow.LastModified < item.LastModified)
                {
                    masterRow.MName = item.MName;
                    masterRow.LastModified = item.LastModified;
                    masterRow.Address = item.Address;
                    masterRow.AllTabaghe = item.AllTabaghe;
                    masterRow.AllVahed = item.AllVahed;
                    masterRow.BranchID = item.BranchID;
                    masterRow.Code = item.Code;
                    masterRow.CoolA = item.CoolA;
                    masterRow.CoolB = item.CoolB;
                    masterRow.CreationDate = item.CreationDate;
                    masterRow.DatePishForoshTahvil = item.DatePishForoshTahvil;
                    masterRow.Description = item.Description;
                    masterRow.Description_Latin = item.Description_Latin;
                    masterRow.Det = item.Det;
                    masterRow.Ejare = item.Ejare;
                    masterRow.Ejtemat = item.Ejtemat;
                    masterRow.Eslahi = item.Eslahi;
                    masterRow.Estakhr = item.Estakhr;
                    masterRow.FileStatusID = item.FileStatusID;
                    masterRow.FiletypeID = item.FiletypeID;
                    masterRow.Forosh = item.Forosh;
                    masterRow.Gharbi = item.Gharbi;
                    masterRow.Hayat = item.Hayat;
                    masterRow.IsDelete = item.IsDelete;
                    masterRow.IsDeleteDateTime = item.IsDeleteDateTime;
                    masterRow.Jakozi = item.Jakozi;
                    masterRow.Jaroo = item.Jaroo;
                    masterRow.Jonobi = item.Jonobi;
                    masterRow.MapLat = item.MapLat;
                    masterRow.MapLong = item.MapLong;
                    masterRow.Masahat = item.Masahat;
                    masterRow.ModificationDate = item.ModificationDate;
                    masterRow.MTel1 = item.MTel1;
                    masterRow.MTel2 = item.MTel2;
                    masterRow.MTel3 = item.MTel3;
                    masterRow.Pakage = item.Pakage;
                    masterRow.Pasio = item.Pasio;
                    masterRow.PishForosh = item.PishForosh;
                    masterRow.Plak = item.Plak;
                    masterRow.PMotor = item.PMotor;
                    masterRow.RegionID = item.RegionID;
                    masterRow.ReturnTime = item.ReturnTime;
                    masterRow.RoofGarden = item.RoofGarden;
                    masterRow.RowVersion = item.RowVersion;
                    masterRow.SanadStatusID = item.SanadStatusID;
                    masterRow.Senbana = item.Senbana;
                    masterRow.Sharghi = item.Sharghi;
                    masterRow.Shofazh = item.Shofazh;
                    masterRow.Shomali = item.Shomali;
                    masterRow.SName = item.SName;
                    masterRow.Sona = item.Sona;
                    masterRow.Spilit = item.Spilit;
                    masterRow.STel1 = item.STel1;
                    masterRow.STel2 = item.STel2;
                    masterRow.STel3 = item.STel3;
                    masterRow.Tajmi = item.Tajmi;
                    masterRow.Tol = item.Tol;
                    masterRow.Vahed = item.Vahed;
                    masterRow.ViewID = item.ViewID;
                    masterRow.Warmer = item.Warmer;
                    masterRow.UserIDCreator = item.UserIDCreator;
                    masterRow.UserIDModifier = item.UserIDModifier;
                    masterRow.ViewPhoneDate = item.ViewPhoneDate;
                    masterRow.ViewPhoneUserID = item.ViewPhoneUserID;

                    count_push++;
                }
                var Allownerdetlocal=local.OwnerDets_List.Where(x=>x.OwnerMasterID==item.MasterID).ToList();
                var Allownerdetmaster=master.OwnerDets_List.Where(x => x.OwnerMasterID == item.MasterID).ToList();
                if (Allownerdetlocal.Count() != Allownerdetmaster.Count())
                {
                    var alllocalmasterid=Allownerdetlocal.Select(x=>x.MasterID).ToList();
                    var removemasterid = Allownerdetmaster.Where(x => !alllocalmasterid.Contains(x.MasterID)).ToList();
                    if(removemasterid!=null && removemasterid.Count() > 0)
                    {
                        master.OwnerDets_List.RemoveRange(removemasterid);
                    }
                }
                
            }
            var _deleteownerlocal = local.OwnersDeleted_List.ToList();
            if (_deleteownerlocal != null && _deleteownerlocal.Count>0)
            {
                var _deleteowneridlocal = _deleteownerlocal.Select(x => x.OwnerGuiD).ToList();
            if (_deleteowneridlocal != null)
                {
                    var _masterowner = master.Owners_List.Where(x => _deleteowneridlocal.Contains(x.MasterID)).ToList();
                    if (_masterowner != null)
                    {
                        foreach (var item in _deleteownerlocal)
                        {
                            master.OwnersDeleted_List.Add(new OwnersDeleted()
                            {
                                ID= Guid.NewGuid(),
                                OwnerGuiD= item.OwnerGuiD,
                                LastModified=DateTime.UtcNow,
                            });
                        }
                        master.Owners_List.RemoveRange(_masterowner);
                        local.OwnersDeleted_List.RemoveRange(_deleteownerlocal);
                        
                    }

                }
            }
            
        }

        private static void PullOwners(RealEstateContext local, RealEstateContext master, DateTime lastPull)
        {
            var changedMaster = master.Owners_List
                .Where(x => x.LastModified > lastPull)
                .ToList();
            count_changedMaster_owner= changedMaster.Count;
            AppLogger.Log("count_changedMaster_owner = " + count_changedMaster_owner);
            foreach (var item in changedMaster)
            {
                var localRow = local.Owners_List
                    .FirstOrDefault(x => x.MasterID == item.MasterID);

                if (localRow == null)
                {
                    local.Owners_List.Add(new Owners
                    {
                        MasterID = item.MasterID,
                        MName = item.MName,
                        LastModified = item.LastModified,
                        Address = item.Address,
                        AllTabaghe = item.AllTabaghe,
                        AllVahed = item.AllVahed,
                        BranchID = item.BranchID,
                        Code = item.Code,
                        CoolA = item.CoolA,
                        CoolB = item.CoolB,
                        CreationDate = item.CreationDate,
                        DatePishForoshTahvil = item.DatePishForoshTahvil,
                        Description = item.Description,
                        Description_Latin = item.Description_Latin,
                        Det = item.Det,
                        Ejare = item.Ejare,
                        Ejtemat = item.Ejtemat,
                        Eslahi = item.Eslahi,
                        Estakhr = item.Estakhr,
                        FileStatusID = item.FileStatusID,
                        FiletypeID = item.FiletypeID,
                        Forosh = item.Forosh,
                        Gharbi = item.Gharbi,
                        Hayat = item.Hayat,
                        IsDelete = item.IsDelete,
                        IsDeleteDateTime = item.IsDeleteDateTime,
                        Jakozi = item.Jakozi,
                        Jaroo = item.Jaroo,
                        Jonobi = item.Jonobi,
                        MapLat = item.MapLat,
                        MapLong = item.MapLong,
                        Masahat = item.Masahat,
                        ModificationDate = item.ModificationDate,
                        MTel1 = item.MTel1,
                        MTel2 = item.MTel2,
                        MTel3 = item.MTel3,
                        Pakage = item.Pakage,
                        Pasio = item.Pasio,
                        PishForosh = item.PishForosh,
                        Plak = item.Plak,
                        PMotor = item.PMotor,
                        RegionID = item.RegionID,
                        ReturnTime = item.ReturnTime,
                        RoofGarden = item.RoofGarden,
                        RowVersion = item.RowVersion,
                        SanadStatusID = item.SanadStatusID,
                        Senbana = item.Senbana,
                        Sharghi = item.Sharghi,
                        Shofazh = item.Shofazh,
                        Shomali = item.Shomali,
                        SName = item.SName,
                        Sona = item.Sona,
                        Spilit = item.Spilit,
                        STel1 = item.STel1,
                        STel2 = item.STel2,
                        STel3 = item.STel3,
                        Tajmi = item.Tajmi,
                        Tol = item.Tol,
                        Vahed = item.Vahed,
                        ViewID = item.ViewID,
                        Warmer = item.Warmer,
                        UserIDCreator = item.UserIDCreator,
                        UserIDModifier = item.UserIDModifier,
                        ViewPhoneDate = item.ViewPhoneDate,
                        ViewPhoneUserID = item.ViewPhoneUserID,
                    });
                    count_pull++;
                }
                else if (localRow.LastModified < item.LastModified)
                {
                    localRow.MName = item.MName;
                    localRow.LastModified = item.LastModified;
                    localRow.Address = item.Address;
                    localRow.AllTabaghe = item.AllTabaghe;
                    localRow.AllVahed = item.AllVahed;
                    localRow.BranchID = item.BranchID;
                    localRow.Code = item.Code;
                    localRow.CoolA = item.CoolA;
                    localRow.CoolB = item.CoolB;
                    localRow.CreationDate = item.CreationDate;
                    localRow.DatePishForoshTahvil = item.DatePishForoshTahvil;
                    localRow.Description = item.Description;
                    localRow.Description_Latin = item.Description_Latin;
                    localRow.Det = item.Det;
                    localRow.Ejare = item.Ejare;
                    localRow.Ejtemat = item.Ejtemat;
                    localRow.Eslahi = item.Eslahi;
                    localRow.Estakhr = item.Estakhr;
                    localRow.FileStatusID = item.FileStatusID;
                    localRow.FiletypeID = item.FiletypeID;
                    localRow.Forosh = item.Forosh;
                    localRow.Gharbi = item.Gharbi;
                    localRow.Hayat = item.Hayat;
                    localRow.IsDelete = item.IsDelete;
                    localRow.IsDeleteDateTime = item.IsDeleteDateTime;
                    localRow.Jakozi = item.Jakozi;
                    localRow.Jaroo = item.Jaroo;
                    localRow.Jonobi = item.Jonobi;
                    localRow.MapLat = item.MapLat;
                    localRow.MapLong = item.MapLong;
                    localRow.Masahat = item.Masahat;
                    localRow.ModificationDate = item.ModificationDate;
                    localRow.MTel1 = item.MTel1;
                    localRow.MTel2 = item.MTel2;
                    localRow.MTel3 = item.MTel3;
                    localRow.Pakage = item.Pakage;
                    localRow.Pasio = item.Pasio;
                    localRow.PishForosh = item.PishForosh;
                    localRow.Plak = item.Plak;
                    localRow.PMotor = item.PMotor;
                    localRow.RegionID = item.RegionID;
                    localRow.ReturnTime = item.ReturnTime;
                    localRow.RoofGarden = item.RoofGarden;
                    localRow.RowVersion = item.RowVersion;
                    localRow.SanadStatusID = item.SanadStatusID;
                    localRow.Senbana = item.Senbana;
                    localRow.Sharghi = item.Sharghi;
                    localRow.Shofazh = item.Shofazh;
                    localRow.Shomali = item.Shomali;
                    localRow.SName = item.SName;
                    localRow.Sona = item.Sona;
                    localRow.Spilit = item.Spilit;
                    localRow.STel1 = item.STel1;
                    localRow.STel2 = item.STel2;
                    localRow.STel3 = item.STel3;
                    localRow.Tajmi = item.Tajmi;
                    localRow.Tol = item.Tol;
                    localRow.Vahed = item.Vahed;
                    localRow.ViewID = item.ViewID;
                    localRow.Warmer = item.Warmer;
                    localRow.UserIDCreator = item.UserIDCreator;
                    localRow.UserIDModifier = item.UserIDModifier;
                    localRow.ViewPhoneDate = item.ViewPhoneDate;
                    localRow.ViewPhoneUserID = item.ViewPhoneUserID;

                    count_pull++;
                }

                var Allownerdetlocal = local.OwnerDets_List.Where(x => x.OwnerMasterID == item.MasterID).ToList();
                var Allownerdetmaster = master.OwnerDets_List.Where(x => x.OwnerMasterID == item.MasterID).ToList();
                if (Allownerdetlocal.Count() != Allownerdetmaster.Count())
                {
                    var allmastermasterid = Allownerdetmaster.Select(x => x.MasterID).ToList();
                    var removemasterid = Allownerdetlocal.Where(x => !allmastermasterid.Contains(x.MasterID)).ToList();
                    if (removemasterid != null && removemasterid.Count() > 0)
                    {
                        local.OwnerDets_List.RemoveRange(removemasterid);
                    }
                }
                
            }
        }

        private static void PushOwnerDet(RealEstateContext local, RealEstateContext master, DateTime lastPush)
        {
            var changedLocal = local.OwnerDets_List
                .Where(x => x.LastModified > lastPush)
                .ToList();
            count_changedLocal_ownerdet = changedLocal.Count;
            AppLogger.Log("count_changedLocal_ownerdet = " + count_changedLocal_ownerdet);
            foreach (var item in changedLocal)
            {
                if (item.MasterID == Guid.Empty)
                    item.MasterID = Guid.NewGuid();

                var masterRow = master.OwnerDets_List
                    .FirstOrDefault(x => x.MasterID == item.MasterID);

                if (masterRow == null)
                {
                    var master_owner = master.Owners_List.Where(x => x.MasterID == item.OwnerMasterID).FirstOrDefault();
                    master.OwnerDets_List.Add(new OwnerDet
                    {

                        OwnerID = master_owner.ID,
                        MasterID = item.MasterID,
                        Zirbana = item.Zirbana,
                        LastModified = item.LastModified,
                        Anbari = item.Anbari,
                        Asansor = item.Asansor,
                        Balkon = item.Balkon,
                        BranchID = item.BranchID,
                        CreationDate = item.CreationDate,
                        Description = item.Description,
                        Description_Latin = item.Description_Latin,
                        KabinetID = item.KabinetID,
                        KafposhID = item.KafposhID,
                        Khab = item.Khab,
                        ModificationDate = item.ModificationDate,
                        OwnerMasterID = item.OwnerMasterID,
                        Parking = item.Parking,
                        Price1 = item.Price1,
                        Price2 = item.Price2,
                        RowVersion = item.RowVersion,
                        ServiceWcID = item.ServiceWcID,
                        Tabaghe = item.Tabaghe,
                        UserIDCreator = item.UserIDCreator,
                        UserIDModifier = item.UserIDModifier,
                        

                    });

                    count_push++;
                }
                else if (masterRow.LastModified < item.LastModified)
                {

                    masterRow.MasterID = item.MasterID;
                    masterRow.Zirbana = item.Zirbana;
                    masterRow.LastModified = item.LastModified;
                    masterRow.Anbari = item.Anbari;
                    masterRow.Asansor = item.Asansor;
                    masterRow.Balkon = item.Balkon;
                    masterRow.BranchID = item.BranchID;
                    masterRow.CreationDate = item.CreationDate;
                    masterRow.Description = item.Description;
                    masterRow.Description_Latin = item.Description_Latin;
                    masterRow.KabinetID = item.KabinetID;
                    masterRow.KafposhID = item.KafposhID;
                    masterRow.Khab = item.Khab;
                    masterRow.ModificationDate = item.ModificationDate;
                    masterRow.OwnerMasterID = item.OwnerMasterID;
                    masterRow.Parking = item.Parking;
                    masterRow.Price1 = item.Price1;
                    masterRow.Price2 = item.Price2;
                    masterRow.RowVersion = item.RowVersion;
                    masterRow.ServiceWcID = item.ServiceWcID;
                    masterRow.Tabaghe = item.Tabaghe;
                    masterRow.UserIDCreator = item.UserIDCreator;
                    masterRow.UserIDModifier = item.UserIDModifier;

                    count_push++;
                }
                
            }

        }

        private static void PullOwnerDet(RealEstateContext local, RealEstateContext master, DateTime lastPull)
        {
            var changedMaster = master.OwnerDets_List
                .Where(x => x.LastModified > lastPull)
                .ToList();
            count_changedMaster_ownerdet= changedMaster.Count;
            AppLogger.Log("count_changedMaster_ownerdet = " + count_changedMaster_ownerdet);
            foreach (var item in changedMaster)
            {
                var localRow = local.OwnerDets_List
                    .FirstOrDefault(x => x.MasterID == item.MasterID);

                if (localRow == null)
                {
                    var local_owner = local.Owners_List.Where(x => x.MasterID == item.OwnerMasterID).FirstOrDefault();
                    local.OwnerDets_List.Add(new OwnerDet
                    {
                        OwnerID = local_owner.ID,
                        MasterID = item.MasterID,
                        Zirbana = item.Zirbana,
                        LastModified = item.LastModified,
                        Anbari = item.Anbari,
                        Asansor = item.Asansor,
                        Balkon = item.Balkon,
                        BranchID = item.BranchID,
                        CreationDate = item.CreationDate,
                        Description = item.Description,
                        Description_Latin = item.Description_Latin,
                        KabinetID = item.KabinetID,
                        KafposhID = item.KafposhID,
                        Khab = item.Khab,
                        ModificationDate = item.ModificationDate,
                        OwnerMasterID = item.OwnerMasterID,
                        Parking = item.Parking,
                        Price1 = item.Price1,
                        Price2 = item.Price2,
                        RowVersion = item.RowVersion,
                        ServiceWcID = item.ServiceWcID,
                        Tabaghe = item.Tabaghe,
                        UserIDCreator = item.UserIDCreator,
                        UserIDModifier = item.UserIDModifier
                    });
                }
                else if (localRow.LastModified < item.LastModified)
                {
                    localRow.MasterID = item.MasterID;
                    localRow.Zirbana = item.Zirbana;
                    localRow.LastModified = item.LastModified;
                    localRow.Anbari = item.Anbari;
                    localRow.Asansor = item.Asansor;
                    localRow.Balkon = item.Balkon;
                    localRow.BranchID = item.BranchID;
                    localRow.CreationDate = item.CreationDate;
                    localRow.Description = item.Description;
                    localRow.Description_Latin = item.Description_Latin;
                    localRow.KabinetID = item.KabinetID;
                    localRow.KafposhID = item.KafposhID;
                    localRow.Khab = item.Khab;
                    localRow.ModificationDate = item.ModificationDate;
                    localRow.OwnerMasterID = item.OwnerMasterID;
                    localRow.Parking = item.Parking;
                    localRow.Price1 = item.Price1;
                    localRow.Price2 = item.Price2;
                    localRow.RowVersion = item.RowVersion;
                    localRow.ServiceWcID = item.ServiceWcID;
                    localRow.Tabaghe = item.Tabaghe;
                    localRow.UserIDCreator = item.UserIDCreator;
                    localRow.UserIDModifier = item.UserIDModifier;
                }
                count_pull++;
            }
        }
    }
}
