namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_UserName = c.String(),
                        b_RealName = c.String(),
                        b_Sex = c.String(),
                        b_Password = c.String(),
                        b_WorkNum = c.String(),
                        b_Email = c.String(),
                        b_PhoneNum = c.String(),
                        b_Depart_ID = c.String(),
                        b_EntryDate = c.String(),
                        b_Role_ID = c.String(),
                        b_Reward_Auth_ID = c.String(),
                        b_Ranking = c.String(),
                        b_Create_Time = c.DateTime(nullable: false),
                        b_Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_User");
        }
    }
}
