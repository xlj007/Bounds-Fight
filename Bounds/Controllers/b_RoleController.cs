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
    public class b_RoleController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Role
        public ActionResult Index()
        {
            return View(db.b_Role.ToList());
        }

        // GET: b_Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Role b_Role = db.b_Role.Find(id);
            if (b_Role == null)
            {
                return HttpNotFound();
            }
            return View(b_Role);
        }

        // GET: b_Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Role/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Role_Name,b_Role_Description,Created_Time,Updated_Time")] b_Role b_Role)
        {
            if (ModelState.IsValid)
            {
                db.b_Role.Add(b_Role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Role);
        }

        // GET: b_Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Role b_Role = db.b_Role.Find(id);
            if (b_Role == null)
            {
                return HttpNotFound();
            }
            return View(b_Role);
        }

        // POST: b_Role/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Role_Name,b_Role_Description,Created_Time,Updated_Time")] b_Role b_Role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Role);
        }

        // GET: b_Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Role b_Role = db.b_Role.Find(id);
            if (b_Role == null)
            {
                return HttpNotFound();
            }
            return View(b_Role);
        }

        // POST: b_Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Role b_Role = db.b_Role.Find(id);
            db.b_Role.Remove(b_Role);
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
