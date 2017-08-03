namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeattence_fixworknum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_WorkNum", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_WorkNum", c => c.Int());
        }
    }
}
