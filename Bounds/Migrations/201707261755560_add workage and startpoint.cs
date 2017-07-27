namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addworkageandstartpoint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_StartPoint",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_StartPoint_Value = c.Int(nullable: false),
                        b_Enterprise = c.String(),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.b_WorkAge",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Balance_Type = c.Int(nullable: false),
                        b_Point_Value = c.Int(nullable: false),
                        b_End_Date = c.String(),
                        b_Enterprise = c.String(),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_WorkAge");
            DropTable("dbo.b_StartPoint");
        }
    }
}
