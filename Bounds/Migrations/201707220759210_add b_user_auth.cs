namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addb_user_auth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_User_Auth",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    b_User_ID = c.Int(nullable: false),
                    b_Auth_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);

        }
        
        public override void Down()
        {
            DropTable("dbo.b_User_Auth");
        }
    }
}
