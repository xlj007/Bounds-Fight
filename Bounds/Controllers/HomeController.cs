using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using Bounds.Utils;
using System.Text;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class HomeController : Controller
    {
        private BoundsContext db = new BoundsContext();
        public ActionResult Index()
        {
            int user_id = (Session["User"] as b_User).ID;
            var my_check_value = (from cu in db.b_Check_User
                                  join u in db.b_User on cu.b_User_ID equals u.ID
                                  where u.ID == user_id
                                  select cu).Select(x => x.b_Check_Type).Distinct().ToList();

            StringBuilder sb = new StringBuilder();
            if (my_check_value.Contains(1))//如果是初审人
            {
                sb.Append("(a.b_Status = 0 and a.b_First_Check_ID = ");
                sb.Append(user_id);
                sb.Append(")");
            }
            else if (my_check_value.Contains(2))//如果是终审人
            {
                if (sb.Length > 0)
                {
                    sb.Append(" or ");
                }
                sb.Append("(a.b_Status = 2 and a.b_Final_Check_ID = ");
                sb.Append(user_id);
                sb.Append(")");
            }
            int nCount = 0;
            //组SQL
            if (sb.Length > 0)
            {
                string strSQLGetRecords = @"select a.ID,a.b_Event_Date, a.b_Record_Time, a.b_Subject, Convert(varchar(10),Sum(f.b_A_Point)) as b_A_Point, Convert(varchar(10),Sum(f.b_B_Point)) as b_B_Point, Convert(varchar(10),Sum(f.b_Value_Point)) as b_Value_Point, Count(f.b_User_ID) as b_PeopleCount_Value,b.b_RealName as b_First_Check_Name, c.b_RealName as b_Final_Check_Name, d.b_RealName as b_Recorder_Name 
                        from b_Point as a join b_User as b on a.b_First_Check_ID = b.ID
                        join b_User as c on a.b_Final_Check_ID = c.ID
                        join b_User as d on a.b_Recorder_ID = d.ID
                        join b_Point_Event as e on e.b_Point_ID = a.ID
                        join b_Point_Event_Member as f on f.b_Point_Event_ID = e.ID
                        Where " + sb.ToString() + " Group by a.ID, a.b_Event_Date, a.b_Record_Time, a.b_Subject,b.b_RealName,c.b_RealName, d.b_RealName";
                IEnumerable<Point_Record_Model> record = db.Database.SqlQuery<Point_Record_Model>(strSQLGetRecords);
                nCount = record.Count();
            }
            ViewBag.CheckCount = nCount;
            return View();
        }
    }
}