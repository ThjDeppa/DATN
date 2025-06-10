namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateVouchers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Voucher", "Value", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Voucher", "MinimunValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Voucher", "MinimunValue", c => c.Single(nullable: false));
            AlterColumn("dbo.tb_Voucher", "Value", c => c.Single(nullable: false));
        }
    }
}
