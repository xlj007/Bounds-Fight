namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Attence",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Attence_Name = c.String(),
                        b_ShaoXiu = c.Int(nullable: false),
                        b_BingJia = c.Int(nullable: false),
                        b_QuanQin = c.Int(nullable: false),
                        b_KuangGong = c.Int(nullable: false),
                        b_Others = c.Int(nullable: false),
                        b_QuanQin_Get_FixedBounds = c.Int(nullable: false),
                        b_1_Free = c.Int(nullable: false),
                        b_2_Free = c.Int(nullable: false),
                        b_3_Free = c.Int(nullable: false),
                        b_4_Free = c.Int(nullable: false),
                        b_5_Free = c.Int(nullable: false),
                        b_6_Free = c.Int(nullable: false),
                        b_7_Free = c.Int(nullable: false),
                        b_8_Free = c.Int(nullable: false),
                        b_9_Free = c.Int(nullable: false),
                        b_10_Free = c.Int(nullable: false),
                        b_11_Free = c.Int(nullable: false),
                        b_12_Free = c.Int(nullable: false),
                        b_Enterprise_ID = c.Int(nullable: false),
                        Created_Time = c.DateTime(nullable: false),
                        Update_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Attence");
        }
    }
}
