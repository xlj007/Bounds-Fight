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
    public class b_Cus_GroupController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Cus_Group
        public ActionResult Index()
        {
            return View(db.b_Cus_Group.ToList());
        }

        // GET: b_Cus_Group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Group b_Cus_Group = db.b_Cus_Group.Find(id);
            if (b_Cus_Group == null)
            {
                return HttpNotFound();
            }
            return View(b_Cus_Group);
        }

        // GET: b_Cus_Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Cus_Group/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Cus_Group_Name,b_Cus_Group_Note,b_Enterprise_ID,Created_Time,Updated_Time")] b_Cus_Group b_Cus_Group)
        {
            if (ModelState.IsValid)
            {
                db.b_Cus_Group.Add(b_Cus_Group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Cus_Group);
        }

        // GET: b_Cus_Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Group b_Cus_Group = db.b_Cus_Group.Find(id);
            if (b_Cus_Group == null)
            {
                return HttpNotFound();
            }
            return View(b_Cus_Group);
        }

        // POST: b_Cus_Group/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Cus_Group_Name,b_Cus_Group_Note,b_Enterprise_ID,Created_Time,Updated_Time")] b_Cus_Group b_Cus_Group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Cus_Group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Cus_Group);
        }

        // GET: b_Cus_Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Group b_Cus_Group = db.b_Cus_Group.Find(id);
            if (b_Cus_Group == null)
            {
                return HttpNotFound();
            }
            return View(b_Cus_Group);
        }

        // POST: b_Cus_Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Cus_Group b_Cus_Group = db.b_Cus_Group.Find(id);
            db.b_Cus_Group.Remove(b_Cus_Group);
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
