namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsomefield : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.b_Task_Info", "b_Task_ID", "dbo.b_Task");
            DropIndex("dbo.b_Task_Info", new[] { "b_Task_ID" });
            AddColumn("dbo.b_Task_Info", "b_Task_Name", c => c.String());
            AddColumn("dbo.b_Task_Info", "b_Enterprise", c => c.String());
            AlterColumn("dbo.b_Task_Info", "b_Task_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Task_Info", "b_Task_ID", c => c.Int());
            DropColumn("dbo.b_Task_Info", "b_Enterprise");
            DropColumn("dbo.b_Task_Info", "b_Task_Name");
            CreateIndex("dbo.b_Task_Info", "b_Task_ID");
            AddForeignKey("dbo.b_Task_Info", "b_Task_ID", "dbo.b_Task", "ID");
        }
    }
}
