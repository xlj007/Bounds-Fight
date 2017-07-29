namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfixpointvaluerelationtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Fix_Point_To_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Fix_Point_ID = c.Int(nullable: false),
                        b_User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Fix_Point_To_User");
        }
    }
}
