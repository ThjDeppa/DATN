﻿namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_OrderDetail", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_OrderDetail", "Size");
        }
    }
}
