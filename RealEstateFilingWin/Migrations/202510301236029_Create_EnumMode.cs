namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_EnumMode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kabinets",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kafposhes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ServiceWcs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("FIL.OwnerDet", "KabinetID", c => c.Long());
            AddColumn("FIL.OwnerDet", "ServiceWcID", c => c.Long());
            AddColumn("FIL.OwnerDet", "KafposhID", c => c.Long());
            AddColumn("FIL.Owners", "Hayat", c => c.Boolean(nullable: false));
            AddColumn("FIL.Owners", "Pasio", c => c.Boolean(nullable: false));
            AlterColumn("FIL.OwnerDet", "Tabaghe", c => c.Int());
            AlterColumn("FIL.OwnerDet", "Zirbana", c => c.Int());
            AlterColumn("FIL.OwnerDet", "Khab", c => c.Int());
            AlterColumn("FIL.OwnerDet", "Parking", c => c.Int());
            CreateIndex("FIL.OwnerDet", "KabinetID");
            CreateIndex("FIL.OwnerDet", "ServiceWcID");
            CreateIndex("FIL.OwnerDet", "KafposhID");
            AddForeignKey("FIL.OwnerDet", "KabinetID", "dbo.Kabinets", "ID");
            AddForeignKey("FIL.OwnerDet", "KafposhID", "dbo.Kafposhes", "ID");
            AddForeignKey("FIL.OwnerDet", "ServiceWcID", "dbo.ServiceWcs", "ID");
            DropColumn("FIL.OwnerDet", "Kabinet");
            DropColumn("FIL.OwnerDet", "ServiceWc");
            DropColumn("FIL.OwnerDet", "Kafposh");
        }
        
        public override void Down()
        {
            AddColumn("FIL.OwnerDet", "Kafposh", c => c.String());
            AddColumn("FIL.OwnerDet", "ServiceWc", c => c.String());
            AddColumn("FIL.OwnerDet", "Kabinet", c => c.String());
            DropForeignKey("FIL.OwnerDet", "ServiceWcID", "dbo.ServiceWcs");
            DropForeignKey("FIL.OwnerDet", "KafposhID", "dbo.Kafposhes");
            DropForeignKey("FIL.OwnerDet", "KabinetID", "dbo.Kabinets");
            DropIndex("FIL.OwnerDet", new[] { "KafposhID" });
            DropIndex("FIL.OwnerDet", new[] { "ServiceWcID" });
            DropIndex("FIL.OwnerDet", new[] { "KabinetID" });
            AlterColumn("FIL.OwnerDet", "Parking", c => c.Int(nullable: false));
            AlterColumn("FIL.OwnerDet", "Khab", c => c.Int(nullable: false));
            AlterColumn("FIL.OwnerDet", "Zirbana", c => c.Int(nullable: false));
            AlterColumn("FIL.OwnerDet", "Tabaghe", c => c.Int(nullable: false));
            DropColumn("FIL.Owners", "Pasio");
            DropColumn("FIL.Owners", "Hayat");
            DropColumn("FIL.OwnerDet", "KafposhID");
            DropColumn("FIL.OwnerDet", "ServiceWcID");
            DropColumn("FIL.OwnerDet", "KabinetID");
            DropTable("dbo.ServiceWcs");
            DropTable("dbo.Kafposhes");
            DropTable("dbo.Kabinets");
        }
    }
}
