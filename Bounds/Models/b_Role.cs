using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Role
    {
        [DataType(DataType.Text)]
        [Display(Name = "角色ID")]
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "角色名称")]
        public string b_Role_Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "角色描述")]
        public string b_Role_Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Created_Time { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Updated_Time { get; set; }
    }
}