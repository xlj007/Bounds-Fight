namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeisnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_TheMonth", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.b_Attence_Fix", "b_TheMonth", c => c.String());
        }
    }
}
