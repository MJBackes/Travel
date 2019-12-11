namespace TravelSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Activities", new[] { "Location_Id" });
            AddColumn("dbo.Itineraries", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Itineraries", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Itineraries", "HotelPlaceId", c => c.String());
            DropColumn("dbo.Activities", "Location_Id");
            DropTable("dbo.Locations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Lat = c.Double(),
                        Long = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Activities", "Location_Id", c => c.Guid());
            DropColumn("dbo.Itineraries", "HotelPlaceId");
            DropColumn("dbo.Itineraries", "EndDate");
            DropColumn("dbo.Itineraries", "StartDate");
            CreateIndex("dbo.Activities", "Location_Id");
            AddForeignKey("dbo.Activities", "Location_Id", "dbo.Locations", "Id");
        }
    }
}
