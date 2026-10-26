namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Settings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GNR.Settings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        BranchID = c.Guid(),
                        Active = c.Boolean(nullable: false),
                        ftpServer = c.String(),
                        ftpPort = c.Int(nullable: false),
                        ftpUser = c.String(),
                        ftpPass = c.String(),
                        ftpRootPath = c.String(),
                        masterConnectionString = c.String(),
                        IntervalFileUpdate = c.Int(nullable: false),
                        IntervalOwnerUpdate = c.Int(nullable: false),
                        IntervalInfoUpdate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("MNG.Branch", "localRoot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("MNG.Branch", "localRoot");
            DropTable("GNR.Settings");
        }
    }
}
