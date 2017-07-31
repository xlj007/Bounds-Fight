using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Task_Info
    {
        public int ID { get; set; }
        public int b_Task_ID { get; set; }
        [Display(Name = "任务名称")]
        public string b_Task_Name { get; set; }
        [Display(Name = "任务类型")]
        public b_Task_Type b_Task_Type { get; set; }
        [Display(Name = "任务下限")]
        public string b_Task_Limit { get; set; }
        [Display(Name = "任务周期")]
        public b_Task_Cycle b_Task_Cycle { get; set; }
        [Display(Name = "未完成扣分")]
        public int b_UnComplete_Dec { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
        public string b_Enterprise { get; set; }
    }
}