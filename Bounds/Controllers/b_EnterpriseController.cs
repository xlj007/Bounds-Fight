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
    public class b_EnterpriseController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Enterprise
        public ActionResult Index()
        {
            return View(db.b_Enterprise.ToList());
        }

        // GET: b_Enterprise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Enterprise b_Enterprise = db.b_Enterprise.Find(id);
            if (b_Enterprise == null)
            {
                return HttpNotFound();
            }
            return View(b_Enterprise);
        }

        // GET: b_Enterprise/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Enterprise/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Enterprise_Code,b_Name")] b_Enterprise b_Enterprise)
        {
            if (ModelState.IsValid)
            {
                db.b_Enterprise.Add(b_Enterprise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Enterprise);
        }

        // GET: b_Enterprise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Enterprise b_Enterprise = db.b_Enterprise.Find(id);
            if (b_Enterprise == null)
            {
                return HttpNotFound();
            }
            return View(b_Enterprise);
        }

        // POST: b_Enterprise/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Enterprise_Code,b_Name")] b_Enterprise b_Enterprise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Enterprise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Enterprise);
        }

        // GET: b_Enterprise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Enterprise b_Enterprise = db.b_Enterprise.Find(id);
            if (b_Enterprise == null)
            {
                return HttpNotFound();
            }
            return View(b_Enterprise);
        }

        // POST: b_Enterprise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Enterprise b_Enterprise = db.b_Enterprise.Find(id);
            db.b_Enterprise.Remove(b_Enterprise);
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
