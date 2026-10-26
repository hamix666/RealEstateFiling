namespace RealEstateFilingWin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReminderUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MNG.UserReminder",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Long(nullable: false),
                        DateReminder = c.DateTime(nullable: false),
                        ShamsiDateReminder = c.String(),
                        TextReminder = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("MNG.UserReminder");
        }
    }
}
