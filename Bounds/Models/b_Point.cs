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
        public string b_First_Check_ID { get; set; }
        [Display(Name = "初审意见")]
        public string b_First_Note { get; set; }
        [Display(Name = "终审人")]
        public string b_Final_Check_ID { get; set; }
        [Display(Name = "终审意见")]
        public string b_Final_Note { get; set; }
        [Display(Name = "记录人")]
        public int b_Recorder_ID { get; set; }
        [Display(Name = "状态")]
        public b_Status b_Status { get; set; }
        public virtual List<b_Point_Event> b_Point_Event { get; set; }
        public string b_Enterprise { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
        //[Display(Name = "驳回原因")]
        //public string b_Return_Note { get; set; }
        public string TheMonth { get; set; }
    }

    public class b_Point_Record
    {
        public int ID { get; set; }
        [Display(Name = "事件时间")]
        public DateTime b_Event_Date { get; set; }
        [Display(Name = "记录时间")]
        public DateTime b_Record_Time { get; set; }
        [Display(Name = "主题")]
        public string b_Subject { get; set; }
        [Display(Name = "备注")]
        public string b_Note { get; set; }
        [Display(Name = "初审人")]
        public string b_First_Check_Name { get; set; }
        [Display(Name = "初审人")]
        public string b_First_Check_ID { get; set; }
        [Display(Name = "初审意见")]
        public string b_First_Note { get; set; }
        [Display(Name = "终审人")]
        public string b_Final_Check_Name { get; set; }
        [Display(Name = "终审人")]
        public string b_Final_Check_ID { get; set; }
        [Display(Name = "终审意见")]
        public string b_Final_Note { get; set; }
        [Display(Name = "记录人")]
        public string b_Recorder_Name { get; set; }
        [Display(Name = "记录人")]
        public int b_Recorder_ID { get; set; }
        [Display(Name = "状态")]
        public b_Status b_Status { get; set; }
        public virtual List<b_Point_Event> b_Point_Event { get; set; }
        public string b_Enterprise { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
        //[Display(Name = "驳回原因")]
        //public string b_Return_Note { get; set; }
        public string TheMonth { get; set; }
    }

    public class Point_Record_Model
    {
        public int ID { get; set; }
        public DateTime b_Event_Date { get; set; }
        public DateTime b_Record_Time { get; set; }
        public string b_Subject { get; set; }
        public string b_A_Point { get; set; }
        public string b_B_Point { get; set; }
        public string b_Value_Point { get; set; }
        public int b_PeopleCount_Value { get; set; }
        public string b_First_Check_Name { get; set; }
        public string b_Final_Check_Name { get; set; }
        public string b_Recorder_Name { get; set; }
        public b_Status b_Status { get; set; }
    }

    public class Point_Check_Model
    {
        public int ID { get; set; }
        [Display(Name = "奖罚对象")]
        public string b_Point_Object { get; set; }

        [Display(Name = "事件时间")]
        public DateTime b_Event_Date { get; set; }
        [Display(Name = "记录时间")]
        public DateTime b_Record_Time { get; set; }
        [Display(Name = "事件主题")]
        public string b_Subject { get; set; }
        [Display(Name = "事件名称")]
        public string b_Event_Name { get; set; }
        [Display(Name = "A分")]
        public string b_A_Point { get; set; }
        [Display(Name = "B分")]
        public string b_B_Point { get; set; }
        [Display(Name = "产值")]
        public string b_Value_Point { get; set; }
        [Display(Name = "初审人")]
        public string b_First_Check_Name { get; set; }
        [Display(Name = "终审人")]
        public string b_Final_Check_Name { get; set; }
        [Display(Name = "记录人")]
        public string b_Recorder_Name { get; set; }
        [Display(Name = "审核状态")]
        public b_Status b_Status { get; set; }
    }

    public class Value_Check_Model
    {
        public string TheMonth { get; set; }
        public string b_RealName { get; set; }
        public string b_WorkNum { get; set; }
        public string b_C_Value { get; set; }
        public string b_S_Value { get; set; }
        public string b_X_Value { get; set; }
        public string b_Total_Value { get; set; }
    }

    public class Value_Order_Report
    {
        public long ID { get; set; }
        public string TheMonth { get; set; }
        public string b_RealName { get; set; }
        public string b_WorkNum { get; set; }
        public string b_C_Value { get; set; }
        public string b_S_Value { get; set; }
        public string b_X_Value { get; set; }
        public string b_Total_Value { get; set; }
    }
    public class My_Values_Model
    {
        public DateTime b_Event_Date { get; set; }
        public string b_Event_Name { get; set; }
        public int b_Value_Point { get; set; }
        public string FirstCheckName { get; set; }
        public string FinalCheckName { get; set; }
    }

    public class My_Fix_Point_Model
    {
        public string b_Fix_Point_Name { get; set; }
        public int b_Fix_Point_Value { get; set; }
    }
    public class My_Point_Event_Model
    {
        public DateTime b_Event_Date { get; set; }
        public string b_Event_Name { get; set; }
        public int b_A_Point { get; set; }
        public int b_B_Point { get; set; }
        public string b_First_Check_Name { get; set; }
        public string b_Final_Check_Name { get; set; }
    }
    public class My_Point_Others_Model
    {
        public string b_Other_Name { get; set; }
        public int b_Other_Point { get; set; }
    }
}