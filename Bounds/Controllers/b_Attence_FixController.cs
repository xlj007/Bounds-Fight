﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using Bounds.Utils;
using System.IO;
using System.Web.Script.Serialization;

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
        [HttpPost]
        public string Count()
        {
            string strReturn = string.Empty;
            int nEnterprise_id = Session["Enterprise_id"].to_i();
            string strEnterprise_id = Session["Enterprise_id"].ToString();
            //获取考勤配置
            var attence_set = (from att in db.b_Attence
                               where att.b_Enterprise_ID == nEnterprise_id
                               select att).FirstOrDefault();
            if (attence_set == null)
            {
                strReturn = "还未设置考勤配置，请先配置考勤信息再进行计算";
                return strReturn;
            }
            //获取启动积分
            var start_point = (from start in db.b_StartPoint
                               where start.b_Enterprise == strEnterprise_id
                               select start).FirstOrDefault();
            int nStart_Point = start_point == null ? 0 : start_point.b_StartPoint_Value;

            //获取工龄分
            var work_age_point = (from age in db.b_WorkAge
                                  where age.b_Enterprise == strEnterprise_id
                                  select age).FirstOrDefault();
            bool boValidDTEnd = false;
            DateTime dtEnd;
            if (work_age_point != null)
            {
                if (DateTime.TryParse(work_age_point.b_End_Date, out dtEnd))
                {
                    boValidDTEnd = true;
                }
                else
                {
                    boValidDTEnd = false;
                }
            }

            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<b_Attence_Fix>>(stream);
            for (int i = 0; i < list.Count; i++)
            {
                double total_point = 0;//总分
                double attence_point = 0;//考勤得分
                double over_time_point = 0;//加班得分
                b_Attence_Fix fix = list[i];
                if (fix.b_Plan_Attence == fix.b_Actual_Attence)
                {
                    //如果全勤，奖分
                    attence_point += Math.Abs(attence_set.b_QuanQin);
                }
                if (fix.b_Sick_Leave > 0)//病假
                {
                    attence_point -= Math.Abs(attence_set.b_BingJia * ((double)fix.b_Sick_Leave) * 2);
                }
                if (fix.b_Other_Leave > 0)//其他假
                {
                    attence_point -= Math.Abs(attence_set.b_Others * ((double)fix.b_Other_Leave) * 2);
                }
                if (fix.b_Absence > 0)//缺勤
                {
                    attence_point -= Math.Abs(attence_set.b_KuangGong * ((double)fix.b_Absence) * 2);
                }
                if (fix.b_OverTime > 0)//加班
                {
                     over_time_point = Math.Abs(attence_set.b_ShaoXiu * (double)fix.b_OverTime);
                }
                //获取固定分
                string strSQLSel = "select sum(b_Fix_Point_Value) as FixPoint From b_Fix_Point where ID in (select b_Fix_Point_ID From b_Fix_Point_To_User Where b_User_id = " + ((b_User)Session["User"]).ID + ")";
                var strFixPointValue = db.Database.SqlQuery<int?>(strSQLSel);
                int nFixPointValue = 0;
                if (strFixPointValue != null) nFixPointValue = strFixPointValue.FirstOrDefault().to_i();
                //获取工龄分
                if (boValidDTEnd && work_age_point.b_Balance_Type == 0)
                {

                }

                fix.b_Fix_Point = nFixPointValue;
                fix.b_Attence_Point = attence_point;
                fix.b_OverTime_Point = over_time_point;
                fix.b_Total_Point = nFixPointValue + attence_point + over_time_point;
                fix.Create_Time = DateTime.Now;
                fix.Update_Time = DateTime.Now;

                db.b_Attence_Fix.Add(fix);
                db.SaveChanges();
            }

            return strReturn;
        }
    }
}