namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileinfo_masterid : DbMigration
    {
        public override void Up()
        {
            AddColumn("FIL.FileInfo", "MasterID", c => c.Guid(nullable: false));
            AddColumn("FIL.FileInfo", "OwnerID", c => c.Guid(nullable: false));
            AddColumn("FIL.FileInfo", "FileHash", c => c.String());
            AddColumn("FIL.FileInfo", "LastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("FIL.FileInfo", "LastModified");
            DropColumn("FIL.FileInfo", "FileHash");
            DropColumn("FIL.FileInfo", "OwnerID");
            DropColumn("FIL.FileInfo", "MasterID");
        }
    }
}
