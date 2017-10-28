using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Enterprise
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name ="企业代码")]
        public string b_Enterprise_Code { get; set; }
        [DataType(DataType.Text)]
        [Display(Name ="企业名称")]
        public string b_Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "负责人姓名")]
        public string b_Leader_Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "联系方式")]
        public string b_Contact { get; set; }
    }
}