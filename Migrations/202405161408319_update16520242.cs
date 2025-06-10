namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update16520242 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "SeoTitle", c => c.String(maxLength: 250));
            AddColumn("dbo.Promotions", "SeoDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.Promotions", "SeoKeywords", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Brand", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_ProductCategory", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.Promotions", "Alias", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Promotions", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tb_ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tb_Brand", "Alias", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Promotions", "SeoKeywords");
            DropColumn("dbo.Promotions", "SeoDescription");
            DropColumn("dbo.Promotions", "SeoTitle");
        }
    }
}
