using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Cus_Group_Member
    {
        public int ID { get; set; }
        public int b_User_ID { get; set; }
        public virtual b_Cus_Group b_Cus_Group { get; set; }
    }
}