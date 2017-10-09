using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Global_Config
    {
        public int ID { get; set; }
        public int b_Enterprise_ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "记录人加分")]
        public string b_Recorder_Add { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "记录人奖分")]
        public string b_Recorder_Price { get; set; }
        [Display(Name = "创富产值转功分(%)")]
        public double b_ChuangFu_To_Bounds { get; set; }
        [Display(Name = "实产值转功分(%)")]
        public double b_ActualValue_To_Bounds { get; set; }
        [Display(Name = "虚产值转功分(%)")]
        public double b_VirtualValue_To_Bounds { get; set; }
        [Display(Name = "营销金额转功分(%)")]
        public double b_Sale_To_Bounds { get; set; }
        [Display(Name = "加班功分")]
        public string b_Attence_To_Bounds { get; set; }
        [Display(Name = "AB分转换倍数")]
        public double b_A_To_B { get; set; }
        [Display(Name = "奖票打印纸设置")]
        public int b_Price_Paper_Set { get; set; }
        [Display(Name = "签到得分")]
        public double b_SignIn_Bounds { get; set; }
        [Display(Name = "签到时间")]
        public string b_SignIn_Time { get; set; }
        [Display(Name = "固定功分与考勤是否关联")]
        public int b_FixedBounds_ToAttence { get; set; }
        [Display(Name = "奖罚任务考核日期每月")]
        public int b_Check_Date { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
        public List<b_Global_Config_Item> b_Global_Config_Item { get; set; }
    }

    public class b_Global_Config_Edit : b_Global_Config
    {
        public new List<b_Global_Config_Item> b_Global_Config_Item { get; set; }
    }
}