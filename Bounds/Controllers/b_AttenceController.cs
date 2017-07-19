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
    public class b_AttenceController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Attence
        public ActionResult Index()
        {
            return View(db.b_Attence.ToList());
        }

        // GET: b_Attence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Attence b_Attence = db.b_Attence.Find(id);
            if (b_Attence == null)
            {
                return HttpNotFound();
            }
            return View(b_Attence);
        }

        // GET: b_Attence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Attence/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Attence_Name,b_ShaoXiu,b_BingJia,b_QuanQin,b_KuangGong,b_Others,b_QuanQin_Get_FixedBounds,b_1_Free,b_2_Free,b_3_Free,b_4_Free,b_5_Free,b_6_Free,b_7_Free,b_8_Free,b_9_Free,b_10_Free,b_11_Free,b_12_Free,b_Enterprise_ID,Created_Time,Update_Time")] b_Attence b_Attence)
        {
            if (ModelState.IsValid)
            {
                db.b_Attence.Add(b_Attence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Attence);
        }

        // GET: b_Attence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Attence b_Attence = db.b_Attence.Find(id);
            if (b_Attence == null)
            {
                return HttpNotFound();
            }
            return View(b_Attence);
        }

        // POST: b_Attence/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Attence_Name,b_ShaoXiu,b_BingJia,b_QuanQin,b_KuangGong,b_Others,b_QuanQin_Get_FixedBounds,b_1_Free,b_2_Free,b_3_Free,b_4_Free,b_5_Free,b_6_Free,b_7_Free,b_8_Free,b_9_Free,b_10_Free,b_11_Free,b_12_Free,b_Enterprise_ID,Created_Time,Update_Time")] b_Attence b_Attence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Attence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Attence);
        }

        // GET: b_Attence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Attence b_Attence = db.b_Attence.Find(id);
            if (b_Attence == null)
            {
                return HttpNotFound();
            }
            return View(b_Attence);
        }

        // POST: b_Attence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Attence b_Attence = db.b_Attence.Find(id);
            db.b_Attence.Remove(b_Attence);
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
