using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_Cus_ReportController : Controller
    {
        public class CRObject
        {
            public string text { get; set; }
            public int value { get; set; }
        }
        private BoundsContext db = new BoundsContext();

        // GET: b_Cus_Report
        public ActionResult Index()
        {
            GetViewBag();
            int ent_id = Convert.ToInt32(Session["Enterprise_id"]);
            var report_list = db.b_Cus_Report.Where(x => x.b_Enterprise_ID == ent_id).ToList();
            return View(report_list);
        }

        public ActionResult Customer_Report(int? id, string StartTime = "", string EndTime = "")
        {
            //字段：序号，姓名，功分
            //取出考勤，固定功分所得总分
            //计算当月奖罚分总分---只计算通过审核的分数

            b_Cus_Report report = (from cus_report in db.b_Cus_Report
                                   where cus_report.ID == id
                                   select cus_report).FirstOrDefault();

            string strSQLSelIDs = "select b_User_ID From b_Cus_Group_Member Where b_Cus_Group_ID in (select * from f_split((select b_Cus_Group_ID From b_Cus_Report Where ID = " + id + "),',')";
            string strMonth = DateTime.Now.ToString("yyyyMM");
            string strFixMonth = DateTime.Now.ToString("yyyy-MM");
            string strFixCondition = string.Empty;
            string strPrizeCondition = string.Empty;
            if (String.IsNullOrEmpty(StartTime) && String.IsNullOrEmpty(EndTime))
            {
                strFixCondition += " and b_TheMonth='" + strFixMonth + "'";
                strPrizeCondition += " and detail.TheMonth = '" + strMonth + "'";
            }
            else
            {
                if (!string.IsNullOrEmpty(StartTime))
                {
                    strFixCondition += " and b_TheMonth>='" + StartTime + "'";
                    strPrizeCondition += " and detail.TheMonth >= '" + StartTime.Replace("-", "") + "'";
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    strFixCondition += " and b_TheMonth<='" + EndTime + "'";
                    strPrizeCondition += " and detail.TheMonth <= '" + EndTime.Replace("-", "") + "'";
                }
            }

            string strSQLSel_Fix = string.Empty;
            if (report != null && report.b_Add_Bounds == 1)
            {
                strSQLSel_Fix = @"select b_Total_Point,b_RealName,b_User_ID from b_Attence_Fix Where 1=1 " + strFixCondition + " and b_User_ID in (" + strSQLSelIDs + ")";
            }
            else
            {
                strSQLSel_Fix = @"select 0 as b_Total_Point,b_RealName,b_User_ID from b_Attence_Fix Where 1=1 " + strFixCondition + " and b_User_ID in (" + strSQLSelIDs + ")";
            }
            string strSQLSel_Prize = @"select sum(b_Point_Value) as b_Total_Point, max(usr.b_RealName) as b_RealName, b_User_ID from b_Point_Details as detail left join b_User as usr on detail.b_User_ID = usr.ID left join b_Point_Event as event on detail.b_Event_ID = event.ID left join b_Point as point on event.b_Point_ID = point.ID  Where 1=1 " + strPrizeCondition + " and detail.b_User_ID in (" + strSQLSelIDs + ") and point.b_Status = 4) Group by detail.b_User_ID";

            string strSQLSel = @"select ROW_NUMBER() over(order by b_Total_Point DESC) as ID, Convert(varchar(10), d.b_Total_Point) as PointValue, d.b_RealName as UserName From (select sum(b_Total_Point) as b_Total_Point, max(b_RealName) as b_RealName From (" + strSQLSel_Fix + ") union all (" + strSQLSel_Prize + ")) as c Group by b_User_ID) as d";

            Log.logger.Info(strSQLSel);
            IEnumerable<b_Cus_Report_Show> record = db.Database.SqlQuery<b_Cus_Report_Show>(strSQLSel);
            return View(record.ToList());
        }

        // GET: b_Cus_Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Report b_Cus_Report = db.b_Cus_Report.Find(id);
            if (b_Cus_Report == null)
            {
                return HttpNotFound();
            }
            return View(b_Cus_Report);
        }

        private void GetViewBag(int? id = null)
        {
            List<CRObject> list_ReportType = new List<CRObject>()
            {
                new CRObject() { text = "功分排名", value = 0 },
                new CRObject() { text = "平均分排名", value = 1 }
            };
            int ent_id = Convert.ToInt32(Session["Enterprise_id"]);
            List<CRObject> list_ContainGroup = new List<CRObject>();
            var groups = from g in db.b_Cus_Group
                         where g.b_Enterprise_ID == ent_id
                         select g;
            foreach (var group in groups)
            {
                CRObject item = new CRObject();
                item.text = group.b_Cus_Group_Name;
                item.value = group.ID;
                list_ContainGroup.Add(item);
            }
            List<CRObject> list_Points = new List<CRObject>()
            {
                new CRObject() { text = "是", value = 1 },
                new CRObject() { text = "否", value = 0 }
            };

            int nSelReportType = 0;
            string nSelContainGroup = "0";
            int nSelPoints = 1;
            if (id != null)
            {
                var report = db.b_Cus_Report.Find(id);
                nSelReportType = report.b_Cus_Report_Type;
                nSelContainGroup = report.b_Cus_Group_ID;
                nSelPoints = report.b_Add_Bounds;
            }

            ViewBag.ReportType = new SelectList(list_ReportType, "value", "text", nSelReportType);
            ViewBag.ContainGroup = new SelectList(list_ContainGroup, "value", "text", nSelContainGroup);
            ViewBag.GroupSel = nSelContainGroup.Split(',');
            ViewBag.Points = new SelectList(list_Points, "value", "text", nSelPoints);
        }
        // GET: b_Cus_Report/Create
        public ActionResult Create()
        {
            GetViewBag();
            return View();
        }

        // POST: b_Cus_Report/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Cus_Report_Name,b_Cus_Report_Type,b_Cus_Group_ID,b_Add_Bounds,b_Cus_Report_Note,b_Enterprise_ID,Created_Time,Updated_Time")] b_Cus_Report b_Cus_Report)
        {
            if (ModelState.IsValid)
            {
                b_Cus_Report.b_Cus_Report_Type = 0;
                b_Cus_Report.b_Enterprise_ID = Convert.ToInt32(Session["Enterprise_id"]);
                b_Cus_Report.Created_Time = DateTime.Now;
                b_Cus_Report.Updated_Time = DateTime.Now;
                //b_Cus_Report.b_Cus_Group_ID = Request.Form["b_Cus_Group_ID"].Replace(",false", "").Replace(",true", "");

                db.b_Cus_Report.Add(b_Cus_Report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            GetViewBag();
            return View();
        }

        // GET: b_Cus_Report/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Report b_Cus_Report = db.b_Cus_Report.Find(id);
            if (b_Cus_Report == null)
            {
                return HttpNotFound();
            }
            GetViewBag(id);
            return View(b_Cus_Report);
        }

        // POST: b_Cus_Report/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Cus_Report_Name,b_Cus_Report_Type,b_Cus_Group_ID,b_Add_Bounds,b_Cus_Report_Note,b_Enterprise_ID,Created_Time,Updated_Time")] b_Cus_Report b_Cus_Report)
        {
            if (ModelState.IsValid)
            {
                b_Cus_Report.b_Cus_Group_ID = Request.Form["b_Cus_Group_ID"].Replace(",false", "").Replace(",true", "");
                b_Cus_Report.Updated_Time = DateTime.Now;
                b_Cus_Report.b_Cus_Report_Type = 0;
                db.Entry(b_Cus_Report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            GetViewBag(b_Cus_Report.ID);
            return View(b_Cus_Report);
        }

        // GET: b_Cus_Report/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Report b_Cus_Report = db.b_Cus_Report.Find(id);
            if (b_Cus_Report == null)
            {
                return HttpNotFound();
            }
            return View(b_Cus_Report);
        }

        // POST: b_Cus_Report/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Report b_Cus_Report = db.b_Cus_Report.Find(id);
            if (b_Cus_Report == null)
            {
                return HttpNotFound();
            }
            db.b_Cus_Report.Remove(b_Cus_Report);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
