namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Role", "b_Member_Count", c => c.Int());
            AddColumn("dbo.b_Role", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Role", "Discriminator");
            DropColumn("dbo.b_Role", "b_Member_Count");
        }
    }
}
