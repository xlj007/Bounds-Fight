namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeb_RealNametype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_RealName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_RealName", c => c.Int());
        }
    }
}
