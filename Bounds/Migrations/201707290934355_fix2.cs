namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Point", "b_First_Check_ID", c => c.String());
            AlterColumn("dbo.b_Point", "b_Final_Check_ID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Point", "b_Final_Check_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.b_Point", "b_First_Check_ID", c => c.Int(nullable: false));
        }
    }
}
