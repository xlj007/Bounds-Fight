using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_Global_Config_Item
    {
        public int ID { get; set; }
        public int b_Global_Config_ID { get; set; }
        public string b_Item_Name { get; set; }
        public string b_Item_Value { get; set; }
        public int b_Item_Type { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}