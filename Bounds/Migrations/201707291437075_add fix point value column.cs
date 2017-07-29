namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfixpointvaluecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Fix_Point", "b_Fix_Point_Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Fix_Point", "b_Fix_Point_Value");
        }
    }
}
