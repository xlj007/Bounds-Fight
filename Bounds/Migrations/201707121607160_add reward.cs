namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Reward",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Reward_Name = c.String(),
                        b_Reward_A = c.Int(nullable: false),
                        b_Reward_B = c.Int(nullable: false),
                        b_Reward_Value = c.Int(nullable: false),
                        b_Enterprise_ID = c.Int(nullable: false),
                        Created_Time = c.DateTime(nullable: false),
                        Updated_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Reward");
        }
    }
}
