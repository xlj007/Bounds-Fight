using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Attence_Fix
    {
        public int ID { get; set; }
        public int? b_User_ID { get; set; }
        [Display(Name = "姓名")]
        public string b_RealName { get; set; }
        [Display(Name = "工号")]
        public string b_WorkNum { get; set; }
        [Display(Name = "月份")]
        public string b_TheMonth { get; set; }
        [Display(Name = "计划出勤天数")]
        public double? b_Plan_Attence { get; set; }
        [Display(Name = "实际出勤天数")]
        public double? b_Actual_Attence { get; set; }
        [Display(Name = "病假天数")]
        public double? b_Sick_Leave { get; set; }
        [Display(Name = "其他假天数")]
        public double? b_Other_Leave { get; set; }
        [Display(Name = "缺勤天数")]
        public double? b_Absence { get; set; }
        [Display(Name = "加班小时")]
        public double? b_OverTime { get; set; }
        [Display(Name ="营销金额")]
        public double? b_SaleAmount { get; set; }
        [Display(Name = "固定功分")]
        public double? b_Fix_Point { get; set; }
        [Display(Name = "考勤得分")]
        public double? b_Attence_Point { get; set; }
        [Display(Name = "加班得分")]
        public double? b_OverTime_Point { get; set; }
        [Display(Name = "营销金额得分")]
        public double? b_Sale_Point { get; set; }
        [Display(Name = "总分")]
        public double? b_Total_Point { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}