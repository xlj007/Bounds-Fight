namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addauth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Auth",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Auth_Name = c.String(),
                        b_Auth_Group_ID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Auth");
        }
    }
}
