using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class BoundsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BoundsContext() : base("name=BoundsContext")
        {
        }

        public System.Data.Entity.DbSet<Bounds.Models.b_User> b_User { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Organize> b_Organize { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Enterprise> b_Enterprise { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Role> b_Role { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Reward> b_Reward { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Global_Config> b_Global_Config { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Attence> b_Attence { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Auth> b_Auth { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Cus_Group> b_Cus_Group { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Cus_Report> b_Cus_Report { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_User_Auth> b_User_Auth { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_User_Role> b_User_Role { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Global_Config_Item> b_Global_Config_Item { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Cus_Group_Member> b_Cus_Group_Member { get; set; }
    }
}
