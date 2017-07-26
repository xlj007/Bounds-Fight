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
using System.Transactions;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_Cus_GroupController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Cus_Group
        public ActionResult Index()
        {
            int ent_id = Convert.ToInt16(Session["Enterprise_id"]);
            return View(db.b_Cus_Group.Include("b_Cus_Group_Member").Where(x => x.b_Enterprise_ID == ent_id).ToList());
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
                b_Cus_Group.Created_Time = DateTime.Now;
                b_Cus_Group.Updated_Time = DateTime.Now;
                b_Cus_Group.b_Enterprise_ID = Convert.ToInt16(Session["Enterprise_id"]);
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
                b_Cus_Group.Updated_Time = DateTime.Now;
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Cus_Group b_Cus_Group = db.b_Cus_Group.Include("b_Cus_Group_Member").Where(x => x.ID == id).FirstOrDefault();
            if (b_Cus_Group == null)
            {
                return HttpNotFound();
            }
            db.b_Cus_Group_Member.RemoveRange(b_Cus_Group.b_Cus_Group_Member);

            db.b_Cus_Group.Remove(b_Cus_Group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetGroupUser(int group_id)
        {
            int ent_id = Session["Enterprise_id"].to_i();
            var user = from u in db.b_User
                       join ur in db.b_Cus_Group_Member.Include("b_Cus_Group") on u.ID equals ur.b_User_ID
                       where ur.b_Cus_Group.ID == group_id && u.b_Enterprise_ID == ent_id
                       select new { u.ID, u.b_UserName };

            return Json(user.ToList());
        }

        [HttpPost]
        public ActionResult SaveGroupUser(int group_id, string user_id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var cur_user = from role in db.b_Cus_Group_Member
                                   where role.b_Cus_Group.ID == group_id
                                   select role;
                    db.b_Cus_Group_Member.RemoveRange(cur_user);

                    string[] arrUserIds = user_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strUserId in arrUserIds)
                    {
                        b_Cus_Group_Member b_cus_group_member = new b_Cus_Group_Member();
                        b_cus_group_member.b_Cus_Group = db.b_Cus_Group.Where(x => x.ID == group_id).FirstOrDefault();
                        b_cus_group_member.b_User_ID = strUserId.to_i();
                        db.b_Cus_Group_Member.Add(b_cus_group_member);
                    }
                    db.SaveChanges();
                    ts.Complete();
                }
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存自定义分组出现错误：" + ex.Message);
                return Json(ex.Message);
            }
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
