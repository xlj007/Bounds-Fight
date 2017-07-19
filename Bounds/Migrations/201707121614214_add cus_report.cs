namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcus_report : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Cus_Report",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Cus_Report_Name = c.String(),
                        b_Cus_Report_Type = c.Int(nullable: false),
                        b_Cus_Group_ID = c.Int(nullable: false),
                        b_Add_Bounds = c.Int(nullable: false),
                        b_Cus_Report_Note = c.String(),
                        b_Enterprise_ID = c.Int(nullable: false),
                        Created_Time = c.DateTime(nullable: false),
                        Updated_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Cus_Report");
        }
    }
}
