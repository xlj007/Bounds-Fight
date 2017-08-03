namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeattence_fixisnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_User_ID", c => c.Int());
            AlterColumn("dbo.b_Attence_Fix", "b_User_Name", c => c.Int());
            AlterColumn("dbo.b_Attence_Fix", "b_User_WorkNum", c => c.Int());
            AlterColumn("dbo.b_Attence_Fix", "b_TheMonth", c => c.String());
            AlterColumn("dbo.b_Attence_Fix", "b_Plan_Attence", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Actual_Attence", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Sick_Leave", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Other_Leave", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Absence", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_OverTime", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_SaleAmount", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Fix_Point", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Attence_Point", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_OverTime_Point", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Sale_Point", c => c.Double());
            AlterColumn("dbo.b_Attence_Fix", "b_Total_Point", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_Total_Point", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Sale_Point", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_OverTime_Point", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Attence_Point", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Fix_Point", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_SaleAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_OverTime", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Absence", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Other_Leave", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Sick_Leave", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Actual_Attence", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_Plan_Attence", c => c.Double(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_TheMonth", c => c.String(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_User_WorkNum", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_User_Name", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Attence_Fix", "b_User_ID", c => c.Int(nullable: false));
        }
    }
}
