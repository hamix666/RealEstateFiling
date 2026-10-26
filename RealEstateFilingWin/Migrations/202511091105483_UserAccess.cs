namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAccess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MNG.AccsessName",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        Name = c.String(),
                        Name_E = c.String(),
                        value = c.Long(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "CUS.CustomerReadOwner",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CustomerID = c.Long(nullable: false),
                        OwnerID = c.Long(nullable: false),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FIL.Owners", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "MNG.UserAccsess",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        AccessID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        MyProperty = c.Int(nullable: false),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("MNG.AccsessName", t => t.AccessID, cascadeDelete: true)
                .ForeignKey("MNG.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.AccessID)
                .Index(t => t.UserID);
            
            CreateTable(
                "MNG.UserOwner",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        OwnerID = c.Long(nullable: false),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("FIL.Owners", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.OwnerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("MNG.UserOwner", "OwnerID", "FIL.Owners");
            DropForeignKey("MNG.UserAccsess", "UserID", "MNG.User");
            DropForeignKey("MNG.UserAccsess", "AccessID", "MNG.AccsessName");
            DropForeignKey("CUS.CustomerReadOwner", "OwnerID", "FIL.Owners");
            DropIndex("MNG.UserOwner", new[] { "OwnerID" });
            DropIndex("MNG.UserAccsess", new[] { "UserID" });
            DropIndex("MNG.UserAccsess", new[] { "AccessID" });
            DropIndex("CUS.CustomerReadOwner", new[] { "OwnerID" });
            DropTable("MNG.UserOwner");
            DropTable("MNG.UserAccsess");
            DropTable("CUS.CustomerReadOwner");
            DropTable("MNG.AccsessName");
        }
    }
}
