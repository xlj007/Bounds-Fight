using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bounds.Models
{
    public class b_Auth
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "权限名称")]
        public string b_Auth_Name { get; set; }
        public int b_Auth_Group_ID { get; set; }
    }
}