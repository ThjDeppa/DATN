namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotions", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Promotion_Product", "StartDate");
            DropColumn("dbo.Promotion_Product", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Promotion_Product", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotion_Product", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Promotions", "EndDate");
            DropColumn("dbo.Promotions", "StartDate");
        }
    }
}
