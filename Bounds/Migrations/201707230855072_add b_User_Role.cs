namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addb_User_Role : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_User_Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_User_Id = c.Int(nullable: false),
                        b_Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_User_Role");
        }
    }
}
