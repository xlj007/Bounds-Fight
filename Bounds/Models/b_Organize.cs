using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Organize
    {
        public int ID { get; set; }
        [Display(Name = "上级部门")]
        public int b_PID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string b_Name { get; set; }
        [Display(Name = "所属企业")]
        public int b_Enterprise_Id { get; set; }

        public IEnumerable<b_Organize> children { get; set; }

        public bool open { get; set; }
    }
}