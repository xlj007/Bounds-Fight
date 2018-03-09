namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_work_age_point_to_attencefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Attence_Fix", "b_Work_Age_Point", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.b_Attence_Fix", "b_Work_Age_Point");
        }
    }
}
