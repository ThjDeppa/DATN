namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update752024 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.tb_ProductDetail", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductDetail", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Product", "Price");
        }
    }
}
