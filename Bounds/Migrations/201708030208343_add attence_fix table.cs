namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattence_fixtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Attence_Fix",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_User_ID = c.Int(nullable: false),
                        b_User_Name = c.Int(nullable: false),
                        b_User_WorkNum = c.Int(nullable: false),
                        b_TheMonth = c.String(),
                        b_Plan_Attence = c.Double(nullable: false),
                        b_Actual_Attence = c.Double(nullable: false),
                        b_Sick_Leave = c.Double(nullable: false),
                        b_Other_Leave = c.Double(nullable: false),
                        b_Absence = c.Double(nullable: false),
                        b_OverTime = c.Double(nullable: false),
                        b_SaleAmount = c.Double(nullable: false),
                        b_Fix_Point = c.Double(nullable: false),
                        b_Attence_Point = c.Double(nullable: false),
                        b_OverTime_Point = c.Double(nullable: false),
                        b_Sale_Point = c.Double(nullable: false),
                        b_Total_Point = c.Double(nullable: false),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.b_Point_Details", "b_Change_Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Point_Details", "b_Change_Status");
            DropTable("dbo.b_Attence_Fix");
        }
    }
}
