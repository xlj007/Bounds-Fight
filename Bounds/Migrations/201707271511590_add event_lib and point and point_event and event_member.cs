namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addevent_libandpointandpoint_eventandevent_member : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Event_Library",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Event_Name = c.String(),
                        b_A_Start = c.Int(nullable: false),
                        b_A_Stop = c.Int(nullable: false),
                        b_B_Start = c.Int(nullable: false),
                        b_B_Stop = c.Int(nullable: false),
                        b_Value_Start = c.Int(nullable: false),
                        b_Value_Stop = c.Int(nullable: false),
                        b_PricePaper_Event = c.Int(nullable: false),
                        b_Enterprise = c.String(),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.b_Point",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Event_Date = c.DateTime(nullable: false),
                        b_Record_Time = c.DateTime(nullable: false),
                        b_Subject = c.String(),
                        b_Note = c.String(),
                        b_First_Note = c.String(),
                        b_Final_Note = c.String(),
                        b_Status = c.Int(nullable: false),
                        b_Enterprise = c.String(),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                        b_Final_Check_ID = c.Int(),
                        b_First_Check_ID = c.Int(),
                        b_Recorder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.b_User", t => t.b_Final_Check_ID)
                .ForeignKey("dbo.b_User", t => t.b_First_Check_ID)
                .ForeignKey("dbo.b_User", t => t.b_Recorder_ID)
                .Index(t => t.b_Final_Check_ID)
                .Index(t => t.b_First_Check_ID)
                .Index(t => t.b_Recorder_ID);
            
            CreateTable(
                "dbo.b_Point_Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Event_Note = c.String(),
                        b_Event_ID = c.Int(),
                        b_Point_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.b_Event_Library", t => t.b_Event_ID)
                .ForeignKey("dbo.b_Point", t => t.b_Point_ID)
                .Index(t => t.b_Event_ID)
                .Index(t => t.b_Point_ID);
            
            CreateTable(
                "dbo.b_Point_Event_Member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_A_Point = c.Int(nullable: false),
                        b_B_Point = c.Int(nullable: false),
                        b_Value_Type = c.Int(nullable: false),
                        b_Value_Point = c.Int(nullable: false),
                        b_Person_Time_Count = c.Int(nullable: false),
                        b_Point_Event_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.b_Point_Event", t => t.b_Point_Event_ID)
                .Index(t => t.b_Point_Event_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.b_Point", "b_Recorder_ID", "dbo.b_User");
            DropForeignKey("dbo.b_Point_Event", "b_Point_ID", "dbo.b_Point");
            DropForeignKey("dbo.b_Point_Event_Member", "b_Point_Event_ID", "dbo.b_Point_Event");
            DropForeignKey("dbo.b_Point_Event", "b_Event_ID", "dbo.b_Event_Library");
            DropForeignKey("dbo.b_Point", "b_First_Check_ID", "dbo.b_User");
            DropForeignKey("dbo.b_Point", "b_Final_Check_ID", "dbo.b_User");
            DropIndex("dbo.b_Point_Event_Member", new[] { "b_Point_Event_ID" });
            DropIndex("dbo.b_Point_Event", new[] { "b_Point_ID" });
            DropIndex("dbo.b_Point_Event", new[] { "b_Event_ID" });
            DropIndex("dbo.b_Point", new[] { "b_Recorder_ID" });
            DropIndex("dbo.b_Point", new[] { "b_First_Check_ID" });
            DropIndex("dbo.b_Point", new[] { "b_Final_Check_ID" });
            DropTable("dbo.b_Point_Event_Member");
            DropTable("dbo.b_Point_Event");
            DropTable("dbo.b_Point");
            DropTable("dbo.b_Event_Library");
        }
    }
}
