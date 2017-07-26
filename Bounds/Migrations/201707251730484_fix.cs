namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.b_Global_Config_Item", name: "b_Global_Config_Edit_ID", newName: "b_Global_Config_ID1");
            RenameIndex(table: "dbo.b_Global_Config_Item", name: "IX_b_Global_Config_Edit_ID", newName: "IX_b_Global_Config_ID1");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.b_Global_Config_Item", name: "IX_b_Global_Config_ID1", newName: "IX_b_Global_Config_Edit_ID");
            RenameColumn(table: "dbo.b_Global_Config_Item", name: "b_Global_Config_ID1", newName: "b_Global_Config_Edit_ID");
        }
    }
}
