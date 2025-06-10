namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "Status");
        }
    }
}
