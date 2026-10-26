namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CUS.Customer",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Tel1 = c.String(),
                        Tel2 = c.String(),
                        Tel3 = c.String(),
                        Title = c.String(),
                        Addres = c.String(),
                        FilterJson = c.String(),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FIL.FileInfo",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        MimeType = c.String(),
                        Size = c.String(),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("FIL.Owners", "ViewPhoneDate", c => c.DateTime());
            AddColumn("FIL.Owners", "ViewPhoneUserID", c => c.Long());
            AddColumn("FIL.Owners", "MapLat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("FIL.Owners", "MapLong", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("FIL.Owners", "MapLong");
            DropColumn("FIL.Owners", "MapLat");
            DropColumn("FIL.Owners", "ViewPhoneUserID");
            DropColumn("FIL.Owners", "ViewPhoneDate");
            DropTable("FIL.FileInfo");
            DropTable("CUS.Customer");
        }
    }
}
