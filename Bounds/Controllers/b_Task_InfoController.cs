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
    public class b_Task_InfoController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Task_Info
        public ActionResult Index()
        {
            return View(db.b_Task_Info.ToList());
        }

        // GET: b_Task_Info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task_Info b_Task_Info = db.b_Task_Info.Find(id);
            if (b_Task_Info == null)
            {
                return HttpNotFound();
            }
            return View(b_Task_Info);
        }

        // GET: b_Task_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Task_Info/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Record()
        {
            if (ModelState.IsValid)
            {
                string[] arrTask_Limit = Request.Form["b_Task_Limit"].ToString().Split(',');
                string[] arrTask_Cycle = Request.Form["b_Task_Cycle"].ToString().Split(',');
                string[] arrUnComplete_Dec = Request.Form["b_UnComplete_Dec"].ToString().Split(',');
                for (int i = 0; i < 4; i++)
                {
                    b_Task_Info b_Task_Info = new b_Task_Info();
                    b_Task_Info.b_Task_Type = (b_Task_Type)i;
                    b_Task_Info.b_Task_Limit = arrTask_Limit[i];
                    b_Task_Info.b_Task_Cycle = (b_Task_Cycle)Convert.ToInt16(arrTask_Cycle[i]);
                    b_Task_Info.b_UnComplete_Dec = Convert.ToInt16(arrUnComplete_Dec[i]);
                    b_Task_Info.Create_Time = DateTime.Now;
                    b_Task_Info.Update_Time = DateTime.Now;
                    
                    db.b_Task_Info.Add(b_Task_Info);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: b_Task_Info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task_Info b_Task_Info = db.b_Task_Info.Find(id);
            if (b_Task_Info == null)
            {
                return HttpNotFound();
            }
            return View(b_Task_Info);
        }

        // POST: b_Task_Info/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Task_Type,b_Task_Limit,b_Task_Cycle,b_UnComplete_Dec,Create_Time,Update_Time")] b_Task_Info b_Task_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Task_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Task_Info);
        }

        // GET: b_Task_Info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task_Info b_Task_Info = db.b_Task_Info.Find(id);
            if (b_Task_Info == null)
            {
                return HttpNotFound();
            }
            return View(b_Task_Info);
        }

        // POST: b_Task_Info/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task_Info b_Task_Info = db.b_Task_Info.Find(id);
            if (b_Task_Info == null)
            {
                return HttpNotFound();
            }
            db.b_Task_Info.Remove(b_Task_Info);
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
