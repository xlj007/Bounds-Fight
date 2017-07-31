namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixsth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Task_To_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Task_ID = c.Int(nullable: false),
                        b_User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.b_Task", "b_Enterprise", c => c.String());
            AddColumn("dbo.b_Task", "Create_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.b_Task", "Update_Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Task", "Update_Time");
            DropColumn("dbo.b_Task", "Create_Time");
            DropColumn("dbo.b_Task", "b_Enterprise");
            DropTable("dbo.b_Task_To_User");
        }
    }
}
