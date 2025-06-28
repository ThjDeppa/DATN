namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVoucherModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Voucher", "Alias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Voucher", "Alias", c => c.String(maxLength: 150));
        }
    }
}
