namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changerelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.b_Point", "b_Final_Check_ID", "dbo.b_User");
            DropForeignKey("dbo.b_Point", "b_First_Check_ID", "dbo.b_User");
            DropForeignKey("dbo.b_Point_Event", "b_Event_ID", "dbo.b_Event_Library");
            DropForeignKey("dbo.b_Point_Event_Member", "b_User_ID", "dbo.b_User");
            DropIndex("dbo.b_Point", new[] { "b_Final_Check_ID" });
            DropIndex("dbo.b_Point", new[] { "b_First_Check_ID" });
            DropIndex("dbo.b_Point_Event", new[] { "b_Event_ID" });
            DropIndex("dbo.b_Point_Event_Member", new[] { "b_User_ID" });
            AlterColumn("dbo.b_Point", "b_Final_Check_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Point", "b_First_Check_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Point_Event", "b_Event_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Point_Event_Member", "b_User_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Point_Event_Member", "b_User_ID", c => c.Int());
            AlterColumn("dbo.b_Point_Event", "b_Event_ID", c => c.Int());
            AlterColumn("dbo.b_Point", "b_First_Check_ID", c => c.Int());
            AlterColumn("dbo.b_Point", "b_Final_Check_ID", c => c.Int());
            CreateIndex("dbo.b_Point_Event_Member", "b_User_ID");
            CreateIndex("dbo.b_Point_Event", "b_Event_ID");
            CreateIndex("dbo.b_Point", "b_First_Check_ID");
            CreateIndex("dbo.b_Point", "b_Final_Check_ID");
            AddForeignKey("dbo.b_Point_Event_Member", "b_User_ID", "dbo.b_User", "ID");
            AddForeignKey("dbo.b_Point_Event", "b_Event_ID", "dbo.b_Event_Library", "ID");
            AddForeignKey("dbo.b_Point", "b_First_Check_ID", "dbo.b_User", "ID");
            AddForeignKey("dbo.b_Point", "b_Final_Check_ID", "dbo.b_User", "ID");
        }
    }
}
