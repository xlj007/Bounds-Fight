using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Task_To_User
    {
        public int ID { get; set; }
        public int b_Task_ID { get; set; }
        public int b_User_ID { get; set; }
    }
}