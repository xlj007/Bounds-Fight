using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class b_User_Role
    {
        public int ID { get; set; }
        public int b_User_Id { get; set; }
        public int b_Role_Id { get; set; }
    }
}