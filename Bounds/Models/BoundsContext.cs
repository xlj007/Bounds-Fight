﻿using System;
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

        public System.Data.Entity.DbSet<Bounds.Models.b_WorkAge> b_WorkAge { get; set; }
       
        public System.Data.Entity.DbSet<Bounds.Models.b_StartPoint> b_StartPoint { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Event_Library> b_Event_Library { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Point> b_Point { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Point_Event> b_Point_Event { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Point_Event_Member> b_Point_Event_Member { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Check_User> b_Check_User { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Fix_Point> b_Fix_Point { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Fix_Point_To_User> b_Fix_Point_To_User { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Point_Details> b_Point_Details { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Task> b_Task { get; set; }
        
        public System.Data.Entity.DbSet<Bounds.Models.b_Task_Info> b_Task_Info { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Task_To_User> b_Task_To_User { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Point_Type_Dic> b_Point_Type_Dic { get; set; }

        public System.Data.Entity.DbSet<Bounds.Models.b_Attence_Fix> b_Attence_Fix { get; set; }
    }
}
