namespace Bounds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addglobalconfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.b_Global_Config",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        b_Enterprise_ID = c.Int(nullable: false),
                        b_Recorder_Add = c.String(),
                        b_Recorder_Price = c.String(),
                        b_ChuangFu_To_Bounds = c.Double(nullable: false),
                        b_ActualValue_To_Bounds = c.Double(nullable: false),
                        b_VirtualValue_To_Bounds = c.Double(nullable: false),
                        b_Sale_To_Bounds = c.Double(nullable: false),
                        b_Attence_To_Bounds = c.String(),
                        b_A_To_B = c.Double(nullable: false),
                        b_Price_Paper_Set = c.Int(nullable: false),
                        b_SignIn_Bounds = c.Double(nullable: false),
                        b_SignIn_Time = c.String(),
                        b_FixedBounds_ToAttence = c.Int(nullable: false),
                        b_Check_Date = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.b_Global_Config");
        }
    }
}
