namespace RealEstateExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelsRework2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Listings", "RealtorId");
            CreateIndex("dbo.Listings", "ListingScheduleTypeId");
            AddForeignKey("dbo.Listings", "ListingScheduleTypeId", "dbo.ListingScheduleTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Listings", "RealtorId", "dbo.Realtors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "RealtorId", "dbo.Realtors");
            DropForeignKey("dbo.Listings", "ListingScheduleTypeId", "dbo.ListingScheduleTypes");
            DropIndex("dbo.Listings", new[] { "ListingScheduleTypeId" });
            DropIndex("dbo.Listings", new[] { "RealtorId" });
        }
    }
}
