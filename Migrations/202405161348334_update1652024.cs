namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1652024 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotion_Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PromotionId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Promotions", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(nullable: false, maxLength: 150),
                        Code = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotion_Product", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.Promotion_Product", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.Promotion_Product", new[] { "PromotionId" });
            DropIndex("dbo.Promotion_Product", new[] { "ProductId" });
            DropTable("dbo.Promotions");
            DropTable("dbo.Promotion_Product");
        }
    }
}
