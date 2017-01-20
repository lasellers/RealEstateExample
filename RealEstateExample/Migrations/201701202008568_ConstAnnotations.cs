namespace RealEstateExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConstAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Realtors", "Name", c => c.String(maxLength: 120));
            AlterColumn("dbo.Realtors", "Phone", c => c.String(maxLength: 220));
            AlterColumn("dbo.Realtors", "Address", c => c.String(maxLength: 132));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Realtors", "Address", c => c.String());
            AlterColumn("dbo.Realtors", "Phone", c => c.String());
            AlterColumn("dbo.Realtors", "Name", c => c.String());
        }
    }
}
