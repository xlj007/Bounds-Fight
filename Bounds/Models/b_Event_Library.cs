using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Event_Library
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "事件名称")]
        public string b_Event_Name { get; set; }
        [Display(Name = "A分")]
        public int b_A_Start { get; set; }
        [Display(Name = "-")]
        public int b_A_Stop { get; set; }
        [Display(Name = "B分")]
        public int b_B_Start { get; set; }
        [Display(Name = "-")]
        public int b_B_Stop { get; set; }
        [Display(Name = "产值")]
        public int b_Value_Start { get; set; }
        [Display(Name = "-")]
        public int b_Value_Stop { get; set; }
        [Display(Name = "奖票事件")]
        public int b_PricePaper_Event { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "企业代码")]
        public string b_Enterprise { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}