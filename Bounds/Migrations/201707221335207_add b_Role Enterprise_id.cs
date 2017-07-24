namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addb_RoleEnterprise_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Role", "b_Enterprise", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Role", "b_Enterprise");
        }
    }
}
