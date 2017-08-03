using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Point_Details
    {
        public int ID { get; set; }
        [Display(Name = "积分类型")]
        public int b_Point_Type { get; set; }
        [Display(Name = "积分值")]
        public double b_Point_Value { get; set; }
        [Display(Name = "事件")]
        public int b_Event_ID { get; set; }
        [Display(Name = "生成时间")]
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
        public int b_User_ID { get; set; }
        public string b_Enterprise { get; set; }
        public string TheMonth { get; set; }
        public int b_Change_Status { get; set; }
        public int b_Recorder_ID { get; set; }
    }
}