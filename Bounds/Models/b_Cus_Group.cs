using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Cus_Group
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "分组名称")]
        public string b_Cus_Group_Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "备注")]
        public string b_Cus_Group_Note { get; set; }
        public int b_Enterprise_ID { get; set; }
        public DateTime Created_Time { get; set; }
        public DateTime Updated_Time { get; set; }
    }
}