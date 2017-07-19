namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Role_Name = c.String(),
                        b_Role_Description = c.String(),
                        Created_Time = c.DateTime(nullable: false),
                        Updated_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Role");
        }
    }
}
