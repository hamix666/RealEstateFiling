namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_useractivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MNG.UserActivity",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Subject = c.String(),
                        NameCustomer = c.String(),
                        UserIDCreator = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UserIDModifier = c.Long(),
                        ModificationDate = c.DateTime(),
                        Description = c.String(),
                        Description_Latin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("MNG.UserActivity");
        }
    }
}
