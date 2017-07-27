using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bounds.Models
{
    public class b_WorkAge
    {
        public int ID { get; set; }
        //结算方式
        public int b_Balance_Type { get; set; }
        //积分值
        public int b_Point_Value { get; set; }
        public string b_End_Date { get; set; }
        public string b_Enterprise { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}