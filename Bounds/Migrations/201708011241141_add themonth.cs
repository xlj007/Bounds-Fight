namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addthemonth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Point", "TheMonth", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Point", "TheMonth");
        }
    }
}
