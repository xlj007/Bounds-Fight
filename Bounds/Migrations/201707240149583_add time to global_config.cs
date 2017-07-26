namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimetoglobal_config : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Global_Config", "Create_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.b_Global_Config", "Update_Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Global_Config", "Update_Time");
            DropColumn("dbo.b_Global_Config", "Create_Time");
        }
    }
}
