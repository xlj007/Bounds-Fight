namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeattence_fixfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.b_Attence_Fix", "b_RealName", c => c.Int());
            AddColumn("dbo.b_Attence_Fix", "b_WorkNum", c => c.Int());
            DropColumn("dbo.b_Attence_Fix", "b_User_Name");
            DropColumn("dbo.b_Attence_Fix", "b_User_WorkNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.b_Attence_Fix", "b_User_WorkNum", c => c.Int());
            AddColumn("dbo.b_Attence_Fix", "b_User_Name", c => c.Int());
            DropColumn("dbo.b_Attence_Fix", "b_WorkNum");
            DropColumn("dbo.b_Attence_Fix", "b_RealName");
        }
    }
}
