namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changerelationagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.b_Point", "b_Recorder_ID", "dbo.b_User");
            DropIndex("dbo.b_Point", new[] { "b_Recorder_ID" });
            AlterColumn("dbo.b_Point", "b_Recorder_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Point", "b_Recorder_ID", c => c.Int());
            CreateIndex("dbo.b_Point", "b_Recorder_ID");
            AddForeignKey("dbo.b_Point", "b_Recorder_ID", "dbo.b_User", "ID");
        }
    }
}
