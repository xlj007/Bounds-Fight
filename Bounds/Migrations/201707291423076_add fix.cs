namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Fix_Point",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Fix_Point_Type = c.Int(nullable: false),
                        b_Fix_Point_Name = c.String(),
                        b_Create_User_ID = c.Int(nullable: false),
                        b_Note = c.String(),
                        Create_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Fix_Point");
        }
    }
}
