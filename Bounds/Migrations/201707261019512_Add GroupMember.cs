namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Cus_Group_Member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_User_ID = c.Int(nullable: false),
                        b_Cus_Group_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.b_Cus_Group", t => t.b_Cus_Group_ID)
                .Index(t => t.b_Cus_Group_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.b_Cus_Group_Member", "b_Cus_Group_ID", "dbo.b_Cus_Group");
            DropIndex("dbo.b_Cus_Group_Member", new[] { "b_Cus_Group_ID" });
            DropTable("dbo.b_Cus_Group_Member");
        }
    }
}
