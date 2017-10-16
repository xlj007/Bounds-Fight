using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using Bounds.Utils;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_Attence_FixController : Controller
    {
        private BoundsContext db = new BoundsContext();
        // GET: b_Attence_Fix
        public ActionResult Index()
        {
            int ent_id = Session["Enterprise_id"].to_i();
            string strMonth = DateTime.Now.ToString("yyyy-MM");
            string strSQLSel = "select IsNull(a.ID, 0) as ID, IsNull(b.ID, 0) as b_User_ID, IsNull(b.b_RealName, '') as b_RealName, IsNull(b.b_WorkNum, '') as b_WorkNum, '" + strMonth + "' as b_TheMonth, IsNull(a.b_Plan_Attence, 0) as b_Plan_Attence, IsNull(a.b_Actual_Attence, 0) b_Actual_Attence, IsNull(a.b_Sick_Leave, 0) b_Sick_Leave, IsNull(a.b_Other_Leave,0) as b_Other_Leave, IsNull(a.b_Absence,0) as b_Absence, IsNull(a.b_OverTime, 0) as b_OverTime, IsNull(a.b_SaleAmount, 0) as b_SaleAmount, IsNull(a.b_Fix_Point,0) as b_Fix_Point, IsNull(a.b_Attence_Point,0) as b_Attence_Point, IsNull(a.b_OverTime_Point,0) as b_OverTime_Point, IsNull(a.b_Sale_Point,0) as b_Sale_Point, IsNull(a.b_Total_Point,0) as b_Total_Point,IsNull(a.Create_Time,'" + DateTime.Now.ToString() + "') as Create_Time, IsNull(a.Update_Time, '" + DateTime.Now.ToString() + "') as Update_Time from (Select * from b_Attence_Fix Where b_TheMonth='" + strMonth + "') as a right join b_User as b on a.b_User_ID = b.ID where b.b_Enterprise_id = " + ent_id;
            var list_attence_fix = db.Database.SqlQuery<b_Attence_Fix>(strSQLSel);
            return View(list_attence_fix);
        }

        public string Count()
        {
            string strReturn = string.Empty;

            return strReturn;
        }
    }
}