namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiallDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MNG.AuditLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        UserID = c.Int(nullable: false),
                        UserIP = c.String(),
                        EntityName = c.String(),
                        EntityId = c.String(),
                        Action = c.String(),
                        Changes = c.String(),
                        ActionTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "GNR.FileStatus",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GNR.FileType",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FIL.OwnerDet",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OwnerID = c.Long(nullable: false),
                        Tabaghe = c.Int(nullable: false),
                        Zirbana = c.Int(nullable: false),
                        Khab = c.Int(nullable: false),
                        Parking = c.Int(nullable: false),
                        Kabinet = c.String(),
                        ServiceWc = c.String(),
                        Kafposh = c.String(),
                        Anbari = c.Boolean(nullable: false),
                        Balkon = c.Boolean(nullable: false),
                        Asansor = c.Boolean(nullable: false),
                        Price1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FIL.Owners", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "FIL.Owners",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        MName = c.String(maxLength: 100),
                        MTel1 = c.String(maxLength: 100),
                        MTel2 = c.String(),
                        MTel3 = c.String(),
                        SName = c.String(),
                        STel1 = c.String(),
                        STel2 = c.String(),
                        STel3 = c.String(),
                        Address = c.String(),
                        Plak = c.String(),
                        Vahed = c.String(),
                        Det = c.String(),
                        DatePishForoshTahvil = c.DateTime(),
                        AllTabaghe = c.Int(),
                        AllVahed = c.Int(),
                        Senbana = c.Int(),
                        Masahat = c.Decimal(precision: 18, scale: 2),
                        Tol = c.Decimal(precision: 18, scale: 2),
                        Eslahi = c.Int(),
                        Shomali = c.Boolean(nullable: false),
                        Jonobi = c.Boolean(nullable: false),
                        Sharghi = c.Boolean(nullable: false),
                        Gharbi = c.Boolean(nullable: false),
                        PishForosh = c.Boolean(nullable: false),
                        Forosh = c.Boolean(nullable: false),
                        Ejare = c.Boolean(nullable: false),
                        CoolA = c.Boolean(nullable: false),
                        CoolB = c.Boolean(nullable: false),
                        Spilit = c.Boolean(nullable: false),
                        Warmer = c.Boolean(nullable: false),
                        Pakage = c.Boolean(nullable: false),
                        Shofazh = c.Boolean(nullable: false),
                        Estakhr = c.Boolean(nullable: false),
                        Sona = c.Boolean(nullable: false),
                        Jakozi = c.Boolean(nullable: false),
                        Jaroo = c.Boolean(nullable: false),
                        Ejtemat = c.Boolean(nullable: false),
                        PMotor = c.Boolean(nullable: false),
                        RoofGarden = c.Boolean(nullable: false),
                        Tajmi = c.Boolean(nullable: false),
                        FiletypeID = c.Long(nullable: false),
                        RegionID = c.Long(nullable: false),
                        FileStatusID = c.Long(nullable: false),
                        SanadStatusID = c.Long(nullable: false),
                        ViewID = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsDeleteDateTime = c.DateTime(),
                        ReturnTime = c.DateTime(),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("GNR.FileStatus", t => t.FileStatusID, cascadeDelete: true)
                .ForeignKey("GNR.FileType", t => t.FiletypeID, cascadeDelete: true)
                .ForeignKey("GNR.Region", t => t.RegionID, cascadeDelete: true)
                .ForeignKey("GNR.SanadStatus", t => t.SanadStatusID, cascadeDelete: true)
                .ForeignKey("GNR.Views", t => t.ViewID, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.MName)
                .Index(t => t.MTel1)
                .Index(t => t.FiletypeID)
                .Index(t => t.RegionID)
                .Index(t => t.FileStatusID)
                .Index(t => t.SanadStatusID)
                .Index(t => t.ViewID);
            
            CreateTable(
                "GNR.Region",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GNR.SanadStatus",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "GNR.Views",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "MNG.User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CodeMeli = c.String(maxLength: 10),
                        Mobile = c.String(maxLength: 11),
                        PhonNumber = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("FIL.OwnerDet", "OwnerID", "FIL.Owners");
            DropForeignKey("FIL.Owners", "ViewID", "GNR.Views");
            DropForeignKey("FIL.Owners", "SanadStatusID", "GNR.SanadStatus");
            DropForeignKey("FIL.Owners", "RegionID", "GNR.Region");
            DropForeignKey("FIL.Owners", "FiletypeID", "GNR.FileType");
            DropForeignKey("FIL.Owners", "FileStatusID", "GNR.FileStatus");
            DropIndex("FIL.Owners", new[] { "ViewID" });
            DropIndex("FIL.Owners", new[] { "SanadStatusID" });
            DropIndex("FIL.Owners", new[] { "FileStatusID" });
            DropIndex("FIL.Owners", new[] { "RegionID" });
            DropIndex("FIL.Owners", new[] { "FiletypeID" });
            DropIndex("FIL.Owners", new[] { "MTel1" });
            DropIndex("FIL.Owners", new[] { "MName" });
            DropIndex("FIL.Owners", new[] { "Code" });
            DropIndex("FIL.OwnerDet", new[] { "OwnerID" });
            DropTable("MNG.User");
            DropTable("GNR.Views");
            DropTable("GNR.SanadStatus");
            DropTable("GNR.Region");
            DropTable("FIL.Owners");
            DropTable("FIL.OwnerDet");
            DropTable("GNR.FileType");
            DropTable("GNR.FileStatus");
            DropTable("MNG.AuditLog");
        }
    }
}
