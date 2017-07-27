using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bounds.Models
{
    public class b_StartPoint
    {
        public int ID { get; set; }
        [Display(Name = "启动分配置")]
        public int b_StartPoint_Value { get; set; }
        public string b_Enterprise { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}