namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addglobal_config_item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Global_Config_Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Global_Config_ID = c.Int(nullable: false),
                        b_Item_Name = c.String(),
                        b_Item_Value = c.String(),
                        b_Item_Type = c.Int(nullable: false),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                        b_Global_Config_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.b_Global_Config", t => t.b_Global_Config_ID1)
                .Index(t => t.b_Global_Config_ID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.b_Global_Config_Item", "b_Global_Config_ID1", "dbo.b_Global_Config");
            DropIndex("dbo.b_Global_Config_Item", new[] { "b_Global_Config_ID1" });
            DropTable("dbo.b_Global_Config_Item");
        }
    }
}
