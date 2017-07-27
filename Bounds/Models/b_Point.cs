using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Point
    {
        public int ID { get; set; }
        [Display(Name ="事件时间")]
        public DateTime b_Event_Date { get; set; }
        [Display(Name = "记录时间")]
        public DateTime b_Record_Time { get; set; }
        [Display(Name = "主题")]
        public string b_Subject { get; set; }
        [Display(Name = "备注")]
        public string b_Note { get; set; }
        [Display(Name = "初审人")]
        public virtual b_User b_First_Check { get; set; }
        [Display(Name = "初审意见")]
        public string b_First_Note { get; set; }
        [Display(Name = "终审人")]
        public virtual b_User b_Final_Check { get; set; }
        [Display(Name = "终审意见")]
        public string b_Final_Note { get; set; }
        [Display(Name = "记录人")]
        public virtual b_User b_Recorder { get; set; }
        [Display(Name = "状态")]
        public b_Status b_Status { get; set; }
        public virtual List<b_Point_Event> b_Point_Event { get; set; }
        public string b_Enterprise { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}