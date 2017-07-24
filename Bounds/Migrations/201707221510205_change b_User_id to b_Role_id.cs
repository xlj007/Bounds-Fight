namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeb_User_idtob_Role_id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_User_Auth", "b_Role_ID", c => c.Int(nullable: false));
            DropColumn("dbo.b_User_Auth", "b_User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.b_User_Auth", "b_User_ID", c => c.Int(nullable: false));
            DropColumn("dbo.b_User_Auth", "b_Role_ID");
        }
    }
}
