﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Fix_Point
    {
        public int ID { get; set; }
        [Display(Name = "积分类型")]
        public b_Fix_Point_Type b_Fix_Point_Type { get; set; }
        [Display(Name ="积分名称")]
        public string b_Fix_Point_Name { get; set; }
        [Display(Name = "固定积分得分")]
        public int b_Fix_Point_Value { get; set; }
        [Display(Name = "创建人")]
        public int b_Create_User_ID { get; set; }
        [Display(Name = "备注")]
        public string b_Note { get; set; }
        [Display(Name = "创建时间")]
        public DateTime Create_Time { get; set; }
        public string b_Enterprise { get; set; }
    }
}