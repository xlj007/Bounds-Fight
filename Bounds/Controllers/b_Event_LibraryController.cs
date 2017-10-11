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
    public class b_Event_LibraryController : Controller
    {
        private BoundsContext db = new BoundsContext();

        [HttpPost]
        public ActionResult GetEvents()
        {
            string strEnterprise = Session["Enterprise_id"].ToString();
            var event_list = from el in db.b_Event_Library
                             where el.b_Enterprise == strEnterprise
                             select el;
            return Json(event_list.ToList());
        }
        private void SetViewBag(string strSel = "1")
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "否", Value = "0", Selected = (strSel == "0")  },
                new SelectListItem() { Text = "是", Value = "1", Selected = (strSel == "1") }
            };
            ViewBag.RadioList = list;
        }
        // GET: b_Event_Library
        public ActionResult Index()
        {
            string strEnterprise = Session["Enterprise_id"].ToString();
            return View(db.b_Event_Library.Where(x=>x.b_Enterprise == strEnterprise).ToList());
        }

        // GET: b_Event_Library/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Event_Library b_Event_Library = db.b_Event_Library.Find(id);
            if (b_Event_Library == null)
            {
                return HttpNotFound();
            }
            return View(b_Event_Library);
        }

        // GET: b_Event_Library/Create
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: b_Event_Library/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Event_Name,b_A_Start,b_A_Stop,b_B_Start,b_B_Stop,b_Value_Start,b_Value_Stop,b_PricePaper_Event,b_Enterprise,Create_Time,Update_Time")] b_Event_Library b_Event_Library)
        {
            b_Event_Library.b_Enterprise = Session["Enterprise_id"].ToString();
            b_Event_Library.Create_Time = DateTime.Now;
            b_Event_Library.Update_Time = DateTime.Now;
            db.b_Event_Library.Add(b_Event_Library);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: b_Event_Library/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Event_Library b_Event_Library = db.b_Event_Library.Find(id);
            if (b_Event_Library == null)
            {
                return HttpNotFound();
            }
            SetViewBag(b_Event_Library.b_PricePaper_Event.ToString());
            return View(b_Event_Library);
        }

        // POST: b_Event_Library/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Event_Name,b_A_Start,b_A_Stop,b_B_Start,b_B_Stop,b_Value_Start,b_Value_Stop,b_PricePaper_Event,b_Enterprise,Create_Time,Update_Time")] b_Event_Library b_Event_Library)
        {
            if (ModelState.IsValid)
            {
                b_Event_Library.Update_Time = DateTime.Now;
                db.Entry(b_Event_Library).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Event_Library);
        }

        // GET: b_Event_Library/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Event_Library b_Event_Library = db.b_Event_Library.Find(id);
            if (b_Event_Library == null)
            {
                return HttpNotFound();
            }

            db.b_Event_Library.Remove(b_Event_Library);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(b_Event_Library);
        }

        // POST: b_Event_Library/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Event_Library b_Event_Library = db.b_Event_Library.Find(id);
            db.b_Event_Library.Remove(b_Event_Library);
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
