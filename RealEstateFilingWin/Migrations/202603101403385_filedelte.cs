namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filedelte : DbMigration
    {
        public override void Up()
        {
            AddColumn("FIL.FileInfo", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("FIL.FileInfo", "IsDelete");
        }
    }
}
