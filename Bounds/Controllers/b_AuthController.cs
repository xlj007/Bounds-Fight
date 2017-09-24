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
    public class b_AuthController : Controller
    {
        private BoundsContext db = new BoundsContext();

        public ActionResult Show(int? role_id)
        {
            List<b_Auth_Edit> list = new List<b_Auth_Edit>();
            List<int> role_list = db.b_User_Auth.Where(role => role.b_Role_ID == role_id).Select(role => role.b_Auth_ID).ToList();
            foreach (var item in db.b_Auth.Where(show => show.b_Show == true).ToList())
            {
                if (role_list.Contains(item.ID))
                {
                    list.Add(item.ChangeToAutEdit(1));
                }
                else
                {
                    list.Add(item.ChangeToAutEdit());
                }
            }
            return View(list);
        }
        // GET: b_Auth
        public ActionResult Index()
        {
            return View(db.b_Auth.Where(show => show.b_Show == true).ToList());
        }

        // GET: b_Auth/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Auth b_Auth = db.b_Auth.Find(id);
            if (b_Auth == null)
            {
                return HttpNotFound();
            }
            return View(b_Auth);
        }

        // GET: b_Auth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Auth/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Auth_Name,b_Auth_Group_ID")] b_Auth b_Auth)
        {
            if (ModelState.IsValid)
            {
                db.b_Auth.Add(b_Auth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Auth);
        }

        // GET: b_Auth/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Auth b_Auth = db.b_Auth.Find(id);
            if (b_Auth == null)
            {
                return HttpNotFound();
            }
            return View(b_Auth);
        }

        // POST: b_Auth/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Auth_Name,b_Auth_Group_ID")] b_Auth b_Auth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Auth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Auth);
        }

        // GET: b_Auth/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Auth b_Auth = db.b_Auth.Find(id);
            if (b_Auth == null)
            {
                return HttpNotFound();
            }
            return View(b_Auth);
        }

        // POST: b_Auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Auth b_Auth = db.b_Auth.Find(id);
            db.b_Auth.Remove(b_Auth);
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
