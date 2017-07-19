namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改审核人员字段类型 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Organize", "b_First_User_id", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Organize", "b_Final_User_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Organize", "b_Final_User_id", c => c.String());
            AlterColumn("dbo.b_Organize", "b_First_User_id", c => c.String());
        }
    }
}
