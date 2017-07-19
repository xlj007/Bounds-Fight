namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addenterprise_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_User", "b_Enterprise_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_User", "b_Enterprise_ID");
        }
    }
}
