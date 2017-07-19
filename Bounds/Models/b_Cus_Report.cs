﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Cus_Report
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "报表名称")]
        public string b_Cus_Report_Name { get; set; }
        [Display(Name = "报表类型")]
        public int b_Cus_Report_Type { get; set; }
        [Display(Name = "包含分组")]
        public int b_Cus_Group_ID { get; set; }
        [Display(Name = "基础分、工龄分计入累计分")]
        public int b_Add_Bounds { get; set; }
        [Display(Name = "备注")]
        public string b_Cus_Report_Note { get; set; }
        public int b_Enterprise_ID { get; set; }
        public DateTime Created_Time { get; set; }
        public DateTime Updated_Time { get; set; }
    }
}