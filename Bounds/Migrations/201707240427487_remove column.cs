namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecolumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.b_Global_Config_Item", "b_Global_Config_ID1", "dbo.b_Global_Config");
            DropIndex("dbo.b_Global_Config_Item", new[] { "b_Global_Config_ID1" });
            DropColumn("dbo.b_Global_Config_Item", "b_Global_Config_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.b_Global_Config_Item", "b_Global_Config_ID1", c => c.Int());
            CreateIndex("dbo.b_Global_Config_Item", "b_Global_Config_ID1");
            AddForeignKey("dbo.b_Global_Config_Item", "b_Global_Config_ID1", "dbo.b_Global_Config", "ID");
        }
    }
}
