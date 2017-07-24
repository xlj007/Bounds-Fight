using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_User
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "用户名")]
        public string b_UserName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "真实姓名")]
        public string b_RealName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "性别")]
        public string b_Sex { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string b_Password { get; set; }
        [Display(Name = "企业代码")]
        public int b_Enterprise_ID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "工号")]
        public string b_WorkNum { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "邮箱")]
        public string b_Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "电话")]
        public string b_PhoneNum { get; set; }
        [Display(Name = "部门")]
        public string b_Depart_ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "入职日期")]
        public string b_EntryDate { get; set; }
        [Display(Name = "角色")]
        public string b_Role_ID { get; set; }
        [Display(Name = "奖扣权限")]
        public string b_Reward_Auth_ID { get; set; }
        [Display(Name = "排名")]
        public string b_Ranking { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "生成时间")]
        public DateTime b_Create_Time { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "更新时间")]
        public DateTime b_Update_Time { get; set; }
    }
}