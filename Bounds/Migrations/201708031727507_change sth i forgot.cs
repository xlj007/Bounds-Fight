namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesthiforgot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Point_Details", "b_Recorder_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Point_Details", "b_Recorder_ID");
        }
    }
}
