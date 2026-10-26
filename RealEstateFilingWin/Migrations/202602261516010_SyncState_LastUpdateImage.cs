namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncState_LastUpdateImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("FIL.SyncState", "LastPushImage", c => c.DateTime());
            AddColumn("FIL.SyncState", "LastPullImage", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("FIL.SyncState", "LastPullImage");
            DropColumn("FIL.SyncState", "LastPushImage");
        }
    }
}
