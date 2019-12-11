namespace TravelSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigragtion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Itineraries", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Itineraries", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Itineraries", "EndDate");
            DropColumn("dbo.Itineraries", "StartDate");
        }
    }
}
