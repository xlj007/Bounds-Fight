using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Point_Event
    {
        public int ID { get; set; }
        [Display(Name = "事件")]
        public virtual b_Event_Library b_Event { get; set; }
        [Display(Name = "事件描述")]
        public string b_Event_Note { get; set; }
        [Display(Name = "参与人")]
        public List<b_Point_Event_Member> b_Point_Event_Member { get; set; }
    }
}