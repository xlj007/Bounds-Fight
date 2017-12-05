namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_reward_field_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Reward", "b_Reward_A", c => c.String());
            AlterColumn("dbo.b_Reward", "b_Reward_B", c => c.String());
            AlterColumn("dbo.b_Reward", "b_Reward_Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Reward", "b_Reward_Value", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Reward", "b_Reward_B", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Reward", "b_Reward_A", c => c.Int(nullable: false));
        }
    }
}
