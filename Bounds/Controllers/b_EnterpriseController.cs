using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using Bounds.Utils;

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
            string strMaxEnterpriseID = db.b_Enterprise.Select(x => x.b_Enterprise_Code).ToList().Max();
            ViewBag.MaxEnterpriseID = strMaxEnterpriseID.to_double() + 1;
            return View();
        }

        // POST: b_Enterprise/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Enterprise_Code,b_Name,b_Leader_Name,b_Contact")] b_Enterprise b_Enterprise)
        {
            if (ModelState.IsValid)
            {
                var enterprise = from e in db.b_Enterprise
                                 where e.b_Enterprise_Code == b_Enterprise.b_Enterprise_Code
                                 select e;
                if (enterprise != null)
                {
                    ViewBag.ErrMsg = "当前企业代码已经存在，请更换企业代码后重新保存。";
                    return View(b_Enterprise);
                }
                b_User user = new b_User();
                user.b_UserName = b_Enterprise.b_Leader_Name;
                user.b_RealName = b_Enterprise.b_Leader_Name;
                user.b_Sex = "0";
                user.b_Password = "E10ADC3949BA59ABBE56E057F20F883E";
                user.b_Role_ID = "1";
                user.b_Create_Time = DateTime.Now;
                user.b_Update_Time = DateTime.Now;
                user.b_Enterprise_ID = b_Enterprise.b_Enterprise_Code.to_i();
                db.b_User.Add(user);

                b_Organize orgniaze = new b_Organize();
                orgniaze.b_PID = 0;
                orgniaze.b_Name = "组织架构";
                orgniaze.b_Enterprise_Id = b_Enterprise.b_Enterprise_Code.to_i();
                orgniaze.open = false;
                db.b_Organize.Add(orgniaze);

                db.b_Enterprise.Add(b_Enterprise);
                db.SaveChanges();

                ViewBag.ErrMsg = string.Empty;
                return RedirectToAction("Index");
            }

            ViewBag.ErrMsg = string.Empty;
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
        public ActionResult Edit([Bind(Include = "ID,b_Enterprise_Code,b_Name,b_Leader_Name,b_Contact")] b_Enterprise b_Enterprise)
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
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Enterprise b_Enterprise = db.b_Enterprise.Find(id);
            db.b_Enterprise.Remove(b_Enterprise);

            b_User user = db.b_User.Where(x=>x.b_Enterprise_ID.ToString()==b_Enterprise.b_Enterprise_Code).FirstOrDefault();
            db.b_User.Remove(user);

            b_Organize organize = db.b_Organize.Where(x => x.b_Enterprise_Id.ToString() == b_Enterprise.b_Enterprise_Code).FirstOrDefault();
            db.b_Organize.Remove(organize);

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
