namespace TravelSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Itinerary_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Itineraries", t => t.Itinerary_Id)
                .Index(t => t.Itinerary_Id);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Travelers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Itineraries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        TimeSpan = c.Time(nullable: false, precision: 7),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        HotelPlaceId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Rating = c.Double(),
                        Comment = c.String(),
                        TravelerId = c.Guid(nullable: false),
                        ActivityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Travelers", t => t.TravelerId, cascadeDelete: true)
                .Index(t => t.TravelerId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.InterestActivities",
                c => new
                    {
                        Interest_Id = c.Int(nullable: false),
                        Activity_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Interest_Id, t.Activity_Id })
                .ForeignKey("dbo.Interests", t => t.Interest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.Activity_Id, cascadeDelete: true)
                .Index(t => t.Interest_Id)
                .Index(t => t.Activity_Id);
            
            CreateTable(
                "dbo.TravelerInterests",
                c => new
                    {
                        Traveler_Id = c.Guid(nullable: false),
                        Interest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Traveler_Id, t.Interest_Id })
                .ForeignKey("dbo.Travelers", t => t.Traveler_Id, cascadeDelete: true)
                .ForeignKey("dbo.Interests", t => t.Interest_Id, cascadeDelete: true)
                .Index(t => t.Traveler_Id)
                .Index(t => t.Interest_Id);
            
            CreateTable(
                "dbo.ItineraryTravelers",
                c => new
                    {
                        Itinerary_Id = c.Guid(nullable: false),
                        Traveler_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Itinerary_Id, t.Traveler_Id })
                .ForeignKey("dbo.Itineraries", t => t.Itinerary_Id, cascadeDelete: true)
                .ForeignKey("dbo.Travelers", t => t.Traveler_Id, cascadeDelete: true)
                .Index(t => t.Itinerary_Id)
                .Index(t => t.Traveler_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "TravelerId", "dbo.Travelers");
            DropForeignKey("dbo.Reviews", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.ItineraryTravelers", "Traveler_Id", "dbo.Travelers");
            DropForeignKey("dbo.ItineraryTravelers", "Itinerary_Id", "dbo.Itineraries");
            DropForeignKey("dbo.Activities", "Itinerary_Id", "dbo.Itineraries");
            DropForeignKey("dbo.TravelerInterests", "Interest_Id", "dbo.Interests");
            DropForeignKey("dbo.TravelerInterests", "Traveler_Id", "dbo.Travelers");
            DropForeignKey("dbo.Travelers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InterestActivities", "Activity_Id", "dbo.Activities");
            DropForeignKey("dbo.InterestActivities", "Interest_Id", "dbo.Interests");
            DropIndex("dbo.ItineraryTravelers", new[] { "Traveler_Id" });
            DropIndex("dbo.ItineraryTravelers", new[] { "Itinerary_Id" });
            DropIndex("dbo.TravelerInterests", new[] { "Interest_Id" });
            DropIndex("dbo.TravelerInterests", new[] { "Traveler_Id" });
            DropIndex("dbo.InterestActivities", new[] { "Activity_Id" });
            DropIndex("dbo.InterestActivities", new[] { "Interest_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "ActivityId" });
            DropIndex("dbo.Reviews", new[] { "TravelerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Travelers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Activities", new[] { "Itinerary_Id" });
            DropTable("dbo.ItineraryTravelers");
            DropTable("dbo.TravelerInterests");
            DropTable("dbo.InterestActivities");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Itineraries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Travelers");
            DropTable("dbo.Interests");
            DropTable("dbo.Activities");
        }
    }
}
