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
    public class b_Global_ConfigController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Global_Config
        public ActionResult Index()
        {
            return View(db.b_Global_Config.ToList());
        }

        // GET: b_Global_Config/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            if (b_Global_Config == null)
            {
                return HttpNotFound();
            }
            return View(b_Global_Config);
        }

        // GET: b_Global_Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Global_Config/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Enterprise_ID,b_Recorder_Add,b_Recorder_Price,b_ChuangFu_To_Bounds,b_ActualValue_To_Bounds,b_VirtualValue_To_Bounds,b_Sale_To_Bounds,b_Attence_To_Bounds,b_A_To_B,b_Price_Paper_Set,b_SignIn_Bounds,b_SignIn_Time,b_FixedBounds_ToAttence,b_Check_Date")] b_Global_Config b_Global_Config)
        {
            if (ModelState.IsValid)
            {
                db.b_Global_Config.Add(b_Global_Config);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Global_Config);
        }

        // GET: b_Global_Config/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            if (b_Global_Config == null)
            {
                return HttpNotFound();
            }
            return View(b_Global_Config);
        }

        // POST: b_Global_Config/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Enterprise_ID,b_Recorder_Add,b_Recorder_Price,b_ChuangFu_To_Bounds,b_ActualValue_To_Bounds,b_VirtualValue_To_Bounds,b_Sale_To_Bounds,b_Attence_To_Bounds,b_A_To_B,b_Price_Paper_Set,b_SignIn_Bounds,b_SignIn_Time,b_FixedBounds_ToAttence,b_Check_Date")] b_Global_Config b_Global_Config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Global_Config).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Global_Config);
        }

        // GET: b_Global_Config/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            if (b_Global_Config == null)
            {
                return HttpNotFound();
            }
            return View(b_Global_Config);
        }

        // POST: b_Global_Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            db.b_Global_Config.Remove(b_Global_Config);
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
