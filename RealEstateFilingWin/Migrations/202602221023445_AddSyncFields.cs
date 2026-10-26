namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSyncFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MNG.Branch",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "FIL.SyncState",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastPush = c.DateTime(),
                        LastPull = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("FIL.Owners", "BranchID", c => c.Guid(nullable: false));
            AddColumn("FIL.Owners", "MasterID", c => c.Guid(nullable: false));
            AddColumn("FIL.Owners", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("FIL.Owners", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("FIL.OwnerDet", "BranchID", c => c.Guid(nullable: false));
            AddColumn("FIL.OwnerDet", "MasterID", c => c.Guid(nullable: false));
            AddColumn("FIL.OwnerDet", "OwnerMasterID", c => c.Guid(nullable: false));
            AddColumn("FIL.OwnerDet", "LastModified", c => c.DateTime(nullable: false));
            AddColumn("FIL.OwnerDet", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("FIL.OwnerDet", "RowVersion");
            DropColumn("FIL.OwnerDet", "LastModified");
            DropColumn("FIL.OwnerDet", "OwnerMasterID");
            DropColumn("FIL.OwnerDet", "MasterID");
            DropColumn("FIL.OwnerDet", "BranchID");
            DropColumn("FIL.Owners", "RowVersion");
            DropColumn("FIL.Owners", "LastModified");
            DropColumn("FIL.Owners", "MasterID");
            DropColumn("FIL.Owners", "BranchID");
            DropTable("FIL.SyncState");
            DropTable("MNG.Branch");
        }
    }
}
