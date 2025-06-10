namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateVoucher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Voucher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(maxLength: 150),
                        Code = c.String(),
                        Quantity = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Type = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Value = c.Single(nullable: false),
                        MinimunValue = c.Single(nullable: false),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 250),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_Order", "MoneyShip", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "VoucherId", c => c.String());
            AddColumn("dbo.tb_Order", "Vouchers_Id", c => c.Int());
            CreateIndex("dbo.tb_Order", "Vouchers_Id");
            AddForeignKey("dbo.tb_Order", "Vouchers_Id", "dbo.tb_Voucher", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Order", "Vouchers_Id", "dbo.tb_Voucher");
            DropIndex("dbo.tb_Order", new[] { "Vouchers_Id" });
            DropColumn("dbo.tb_Order", "Vouchers_Id");
            DropColumn("dbo.tb_Order", "VoucherId");
            DropColumn("dbo.tb_Order", "MoneyShip");
            DropTable("dbo.tb_Voucher");
        }
    }
}
