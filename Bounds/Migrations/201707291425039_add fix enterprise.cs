namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfixenterprise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Fix_Point", "b_Enterprise", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Fix_Point", "b_Enterprise");
        }
    }
}
