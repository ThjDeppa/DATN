﻿namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "PaymentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "PaymentStatus");
        }
    }
}
