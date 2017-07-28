namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepoint_event_member : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Point_Event_Member", "b_User_ID", c => c.Int());
            CreateIndex("dbo.b_Point_Event_Member", "b_User_ID");
            AddForeignKey("dbo.b_Point_Event_Member", "b_User_ID", "dbo.b_User", "ID");
            DropColumn("dbo.b_Point_Event_Member", "b_Person_Time_Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.b_Point_Event_Member", "b_Person_Time_Count", c => c.Int(nullable: false));
            DropForeignKey("dbo.b_Point_Event_Member", "b_User_ID", "dbo.b_User");
            DropIndex("dbo.b_Point_Event_Member", new[] { "b_User_ID" });
            DropColumn("dbo.b_Point_Event_Member", "b_User_ID");
        }
    }
}
