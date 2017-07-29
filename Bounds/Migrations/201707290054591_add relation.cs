namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Check_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_User_ID = c.Int(nullable: false),
                        b_Organize_ID = c.Int(nullable: false),
                        b_Check_Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.b_Organize", "b_First_User_id");
            DropColumn("dbo.b_Organize", "b_Final_User_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.b_Organize", "b_Final_User_id", c => c.Int(nullable: false));
            AddColumn("dbo.b_Organize", "b_First_User_id", c => c.Int(nullable: false));
            DropTable("dbo.b_Check_User");
        }
    }
}
