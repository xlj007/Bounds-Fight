namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Organize",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_PID = c.Int(nullable: false),
                        b_Name = c.Int(nullable: false),
                        b_First_User_id = c.String(),
                        b_Final_User_id = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Organize");
        }
    }
}
