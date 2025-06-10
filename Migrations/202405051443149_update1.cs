namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_News", "CategoryId", "dbo.tb_Category");
            DropForeignKey("dbo.tb_Posts", "CategoryId", "dbo.tb_Category");
            DropIndex("dbo.tb_News", new[] { "CategoryId" });
            DropIndex("dbo.tb_Posts", new[] { "CategoryId" });
            DropColumn("dbo.tb_News", "CategoryId");
            DropColumn("dbo.tb_Posts", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Posts", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.tb_News", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Posts", "CategoryId");
            CreateIndex("dbo.tb_News", "CategoryId");
            AddForeignKey("dbo.tb_Posts", "CategoryId", "dbo.tb_Category", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_News", "CategoryId", "dbo.tb_Category", "Id", cascadeDelete: true);
        }
    }
}
