using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Point_Event_Member
    {
        public int ID { get; set; }
        [Display(Name = "A分")]
        public int b_A_Point { get; set; }
        [Display(Name = "B分")]
        public int b_B_Point { get; set; }
        [Display(Name = "产值类型")]
        public b_Value_Type b_Value_Type { get; set; }
        [Display(Name = "产值")]
        public int b_Value_Point { get; set; }
        [Display(Name = "人次")]
        public int b_Person_Time_Count { get; set; }
    }
}