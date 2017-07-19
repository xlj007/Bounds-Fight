namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnodestatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Organize", "open", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Organize", "open");
        }
    }
}
