namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtalbles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Point_Type_Dic",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Point_Type_ID = c.Int(nullable: false),
                        b_Point_Type_Name = c.String(),
                        Create_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.b_Point_Details", "TheMonth", c => c.String());
            AlterColumn("dbo.b_Point_Details", "b_Point_Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Point_Details", "b_Point_Type", c => c.String());
            DropColumn("dbo.b_Point_Details", "TheMonth");
            DropTable("dbo.b_Point_Type_Dic");
        }
    }
}
