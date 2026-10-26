namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unic_code : DbMigration
    {
        public override void Up()
        {
            DropIndex("FIL.Owners", new[] { "Code" });
            AddColumn("MNG.Branch", "Code", c => c.Int());
            CreateIndex("FIL.Owners", new[] { "Code", "BranchID" }, unique: true, name: "IX_Code");
        }
        
        public override void Down()
        {
            DropIndex("FIL.Owners", "IX_Code");
            DropColumn("MNG.Branch", "Code");
            CreateIndex("FIL.Owners", "Code", unique: true);
        }
    }
}
