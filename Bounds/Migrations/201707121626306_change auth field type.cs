namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeauthfieldtype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Auth", "b_Auth_Group_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Auth", "b_Auth_Group_ID", c => c.String());
        }
    }
}
