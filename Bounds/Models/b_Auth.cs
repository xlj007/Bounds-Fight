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

        public b_Auth_Edit ChangeToAutEdit(int nSelected = 0)
        {
            b_Auth_Edit edit = new b_Auth_Edit();
            edit.ID = this.ID;
            edit.b_Auth_Name = this.b_Auth_Name;
            edit.b_Auth_Group_ID = this.b_Auth_Group_ID;
            edit.nSelected = nSelected;
            return edit;
        }
    }

    public class b_Auth_Edit
    {
        public int ID { get; set; }
        public string b_Auth_Name { get; set; }
        public int b_Auth_Group_ID { get; set; }
        public int nSelected { get; set; }
    }
}