namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Point_Details", "Create_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.b_Point_Details", "Update_Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Point_Details", "Update_Time");
            DropColumn("dbo.b_Point_Details", "Create_Time");
        }
    }
}
