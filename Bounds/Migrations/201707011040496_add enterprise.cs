namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addenterprise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Enterprise",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Enterprise_Code = c.String(),
                        b_Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.b_Organize", "b_Enterprise_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Organize", "b_Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Organize", "b_Name", c => c.Int(nullable: false));
            DropColumn("dbo.b_Organize", "b_Enterprise_Id");
            DropTable("dbo.b_Enterprise");
        }
    }
}
