namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteowner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FIL.OwnersDeleted",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OwnerGuiD = c.Guid(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("FIL.SyncState", "LastDeleteOwner", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("FIL.SyncState", "LastDeleteOwner");
            DropTable("FIL.OwnersDeleted");
        }
    }
}
