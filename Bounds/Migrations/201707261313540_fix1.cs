namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Cus_Report", "b_Cus_Report_Type", c => c.Int(nullable: false));
            AddColumn("dbo.b_Cus_Report", "b_Cus_Group_ID", c => c.Int(nullable: false));
            DropColumn("dbo.b_Cus_Report", "b_Cus_Report_Type_DataGroupField");
            DropColumn("dbo.b_Cus_Report", "b_Cus_Report_Type_DataTextField");
            DropColumn("dbo.b_Cus_Report", "b_Cus_Report_Type_DataValueField");
            DropColumn("dbo.b_Cus_Report", "b_Cus_Group_ID_DataGroupField");
            DropColumn("dbo.b_Cus_Report", "b_Cus_Group_ID_DataTextField");
            DropColumn("dbo.b_Cus_Report", "b_Cus_Group_ID_DataValueField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.b_Cus_Report", "b_Cus_Group_ID_DataValueField", c => c.String());
            AddColumn("dbo.b_Cus_Report", "b_Cus_Group_ID_DataTextField", c => c.String());
            AddColumn("dbo.b_Cus_Report", "b_Cus_Group_ID_DataGroupField", c => c.String());
            AddColumn("dbo.b_Cus_Report", "b_Cus_Report_Type_DataValueField", c => c.String());
            AddColumn("dbo.b_Cus_Report", "b_Cus_Report_Type_DataTextField", c => c.String());
            AddColumn("dbo.b_Cus_Report", "b_Cus_Report_Type_DataGroupField", c => c.String());
            DropColumn("dbo.b_Cus_Report", "b_Cus_Group_ID");
            DropColumn("dbo.b_Cus_Report", "b_Cus_Report_Type");
        }
    }
}
