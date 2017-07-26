namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removevirtual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Global_Config", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.b_Global_Config_Item", "b_Global_Config_Edit_ID", c => c.Int());
            CreateIndex("dbo.b_Global_Config_Item", "b_Global_Config_Edit_ID");
            AddForeignKey("dbo.b_Global_Config_Item", "b_Global_Config_Edit_ID", "dbo.b_Global_Config", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.b_Global_Config_Item", "b_Global_Config_Edit_ID", "dbo.b_Global_Config");
            DropIndex("dbo.b_Global_Config_Item", new[] { "b_Global_Config_Edit_ID" });
            DropColumn("dbo.b_Global_Config_Item", "b_Global_Config_Edit_ID");
            DropColumn("dbo.b_Global_Config", "Discriminator");
        }
    }
}
