namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changereportcolumntype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Cus_Report", "b_Cus_Group_ID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Cus_Report", "b_Cus_Group_ID", c => c.Int(nullable: false));
        }
    }
}
