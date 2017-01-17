namespace RealEstateExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelsRework3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Listings", "ListingScheduleTypeId", "dbo.ListingScheduleTypes");
            DropForeignKey("dbo.Listings", "RealtorId", "dbo.Realtors");
            DropIndex("dbo.Listings", new[] { "RealtorId" });
            DropIndex("dbo.Listings", new[] { "ListingScheduleTypeId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Listings", "ListingScheduleTypeId");
            CreateIndex("dbo.Listings", "RealtorId");
            AddForeignKey("dbo.Listings", "RealtorId", "dbo.Realtors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Listings", "ListingScheduleTypeId", "dbo.ListingScheduleTypes", "Id", cascadeDelete: true);
        }
    }
}
