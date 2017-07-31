namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpointdetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Point_Details",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Point_Type = c.String(),
                        b_Point_Value = c.Double(nullable: false),
                        b_Event_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Point_Details");
        }
    }
}
