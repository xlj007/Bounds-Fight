using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Attence
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "考勤名称")]
        public string b_Attence_Name { get; set; }
        [Display(Name = "少休奖分")]
        public int b_ShaoXiu { get; set; }
        [Display(Name ="病假罚分")]
        public int b_BingJia { get; set; }
        [Display(Name = "全勤奖分")]
        public int b_QuanQin { get; set; }
        [Display(Name = "旷工罚分")]
        public int b_KuangGong { get; set; }
        [Display(Name = "其他假罚分")]
        public int b_Others { get; set; }
        [Display(Name = "满勤获得所有固定功分")]
        public int b_QuanQin_Get_FixedBounds { get; set; }
        [Display(Name = "1月")]
        public int b_1_Free { get; set; }
        [Display(Name = "2月")]
        public int b_2_Free { get; set; }
        [Display(Name = "3月")]
        public int b_3_Free { get; set; }
        [Display(Name = "4月")]
        public int b_4_Free { get; set; }
        [Display(Name = "5月")]
        public int b_5_Free { get; set; }
        [Display(Name = "6月")]
        public int b_6_Free { get; set; }
        [Display(Name = "7月")]
        public int b_7_Free { get; set; }
        [Display(Name = "8月")]
        public int b_8_Free { get; set; }
        [Display(Name = "9月")]
        public int b_9_Free { get; set; }
        [Display(Name = "10月")]
        public int b_10_Free { get; set; }
        [Display(Name = "11月")]
        public int b_11_Free { get; set; }
        [Display(Name = "12月")]
        public int b_12_Free { get; set; }
        public int b_Enterprise_ID { get; set; }
        public DateTime Created_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}