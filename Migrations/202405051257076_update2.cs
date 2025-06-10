namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Product", "Size_ID", "dbo.tb_Sizes");
            DropIndex("dbo.tb_Product", new[] { "Size_ID" });
            AddColumn("dbo.tb_Product", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Product", "BrandId");
            AddForeignKey("dbo.tb_Product", "BrandId", "dbo.tb_Brand", "Id", cascadeDelete: true);
            DropColumn("dbo.tb_Product", "Size_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "Size_ID", c => c.Int());
            DropForeignKey("dbo.tb_Product", "BrandId", "dbo.tb_Brand");
            DropIndex("dbo.tb_Product", new[] { "BrandId" });
            DropColumn("dbo.tb_Product", "BrandId");
            CreateIndex("dbo.tb_Product", "Size_ID");
            AddForeignKey("dbo.tb_Product", "Size_ID", "dbo.tb_Sizes", "ID");
        }
    }
}
