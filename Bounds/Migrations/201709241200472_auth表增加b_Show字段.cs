namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auth表增加b_Show字段 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Auth", "b_Show", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Auth", "b_Show");
        }
    }
}
