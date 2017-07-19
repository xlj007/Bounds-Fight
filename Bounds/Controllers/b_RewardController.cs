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
    public class b_RewardController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Reward
        public ActionResult Index()
        {
            return View(db.b_Reward.ToList());
        }

        // GET: b_Reward/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Reward b_Reward = db.b_Reward.Find(id);
            if (b_Reward == null)
            {
                return HttpNotFound();
            }
            return View(b_Reward);
        }

        // GET: b_Reward/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Reward/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Reward_Name,b_Reward_A,b_Reward_B,b_Reward_Value,b_Enterprise_ID,Created_Time,Updated_Time")] b_Reward b_Reward)
        {
            if (ModelState.IsValid)
            {
                db.b_Reward.Add(b_Reward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Reward);
        }

        // GET: b_Reward/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Reward b_Reward = db.b_Reward.Find(id);
            if (b_Reward == null)
            {
                return HttpNotFound();
            }
            return View(b_Reward);
        }

        // POST: b_Reward/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Reward_Name,b_Reward_A,b_Reward_B,b_Reward_Value,b_Enterprise_ID,Created_Time,Updated_Time")] b_Reward b_Reward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Reward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Reward);
        }

        // GET: b_Reward/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Reward b_Reward = db.b_Reward.Find(id);
            if (b_Reward == null)
            {
                return HttpNotFound();
            }
            return View(b_Reward);
        }

        // POST: b_Reward/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Reward b_Reward = db.b_Reward.Find(id);
            db.b_Reward.Remove(b_Reward);
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
