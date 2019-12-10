namespace TravelSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TravelSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TravelSite.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Interests.AddOrUpdate(
            new Models.Interest { Name = "Restaurant", Value = "restaurant" },
            new Models.Interest { Name = "Cafe", Value = "cafe" },
            new Models.Interest { Name = "Bar", Value = "bar" },
            new Models.Interest { Name = "Park", Value = "park" },
            new Models.Interest { Name = "Amusement Park", Value = "amusement_park" },
            new Models.Interest { Name = "Zoo", Value = "zoo" },
            new Models.Interest { Name = "Museum", Value = "museum" },
            new Models.Interest { Name = "Art Gallery", Value = "art_gallery" },
            new Models.Interest { Name = "Spa", Value = "spa" },
            new Models.Interest { Name = "Casino", Value = "casino" },
            new Models.Interest { Name = "Shopping", Value = "shopping_mall" }
        );
        }
    }
}
