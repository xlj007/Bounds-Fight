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
            int ent_id = Convert.ToInt16(Session["Enterprise_id"]);
            return View(db.b_Cus_Report.Where(x=>x.b_Enterprise_ID == ent_id).ToList());
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
                new CRObject() { text = "积分排名", value = 0 },
                new CRObject() { text = "平均分排名", value = 1 }
            };
            int ent_id = Convert.ToInt16(Session["Enterprise_id"]);
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
                b_Cus_Report.b_Enterprise_ID = Convert.ToInt16(Session["Enterprise_id"]);
                b_Cus_Report.Created_Time = DateTime.Now;
                b_Cus_Report.Updated_Time = DateTime.Now;
                b_Cus_Report.b_Cus_Group_ID = Request.Form["b_Cus_Group_ID"].Replace(",false", "").Replace(",true", "");

                db.b_Cus_Report.Add(b_Cus_Report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            GetViewBag();
            return View(b_Cus_Report);
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
