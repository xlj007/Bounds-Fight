namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Task_Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.b_Task_Info",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Task_Type = c.Int(nullable: false),
                        b_Task_Limit = c.String(),
                        b_Task_Cycle = c.Int(nullable: false),
                        b_UnComplete_Dec = c.Int(nullable: false),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                        b_Task_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.b_Task", t => t.b_Task_ID)
                .Index(t => t.b_Task_ID);
            
            AddColumn("dbo.b_Point_Details", "b_User_ID", c => c.Int(nullable: false));
            AddColumn("dbo.b_Point_Details", "b_Enterprise", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.b_Task_Info", "b_Task_ID", "dbo.b_Task");
            DropIndex("dbo.b_Task_Info", new[] { "b_Task_ID" });
            DropColumn("dbo.b_Point_Details", "b_Enterprise");
            DropColumn("dbo.b_Point_Details", "b_User_ID");
            DropTable("dbo.b_Task_Info");
            DropTable("dbo.b_Task");
        }
    }
}
