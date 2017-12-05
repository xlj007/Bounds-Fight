using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Reward
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "权限名称")]
        public string b_Reward_Name { get; set; }
        [Display(Name = "A分权限")]
        public string b_Reward_A { get; set; }
        [Display(Name = "B分权限")]
        public string b_Reward_B { get; set; }
        [Display(Name = "产值权限")]
        public string b_Reward_Value { get; set; }
        public int b_Enterprise_ID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Created_Time { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Updated_Time { get; set; }
    }
}