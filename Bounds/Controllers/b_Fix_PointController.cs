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
    public class b_Fix_PointController : Controller
    {
        private BoundsContext db = new BoundsContext();

        [HttpPost]
        public ActionResult GetFixTypeUser(int fix_type)
        {
            try
            {
                int ent_id = Session["Enterprise_id"].to_i();
                var user = from u in db.b_User
                           join ur in db.b_Fix_Point_To_User on u.ID equals ur.b_User_ID
                           where ur.b_Fix_Point_ID == fix_type && u.b_Enterprise_ID == ent_id
                           select new { u.ID, u.b_UserName };

                return Json(user.ToList());
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取固定功分绑定成员时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult SaveFixTypeUser(int fix_type_id, string user_id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var cur_user = from role in db.b_Fix_Point_To_User
                                   where role.b_Fix_Point_ID == fix_type_id
                                   select role;
                    db.b_Fix_Point_To_User.RemoveRange(cur_user);

                    string[] arrUserIds = user_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strUserId in arrUserIds)
                    {
                        b_Fix_Point_To_User b_fix_point_to_user = new b_Fix_Point_To_User();
                        b_fix_point_to_user.b_Fix_Point_ID = fix_type_id;
                        b_fix_point_to_user.b_User_ID = strUserId.to_i();
                        db.b_Fix_Point_To_User.Add(b_fix_point_to_user);
                    }
                    db.SaveChanges();
                    ts.Complete();
                }
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存固定功分绑定成员时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        // GET: b_Fix_Point
        public ActionResult Index()
        {
            string strEnterprise = Session["Enterprise_id"].ToString();
            var fix_show = from fix in db.b_Fix_Point
                           join user in db.b_User on fix.b_Create_User_ID equals user.ID
                           where fix.b_Enterprise == strEnterprise
                           select new b_Fix_Point_Show
                           {
                               ID = fix.ID,
                               b_Create_User = user.b_RealName,
                               b_Create_User_ID = fix.b_Create_User_ID,
                               b_Enterprise = fix.b_Enterprise,
                               b_Fix_Point_Name = fix.b_Fix_Point_Name,
                               b_Fix_Point_Type = fix.b_Fix_Point_Type,
                               b_Fix_Point_Value = fix.b_Fix_Point_Value,
                               b_Note = fix.b_Note,
                               Create_Time = fix.Create_Time
                           };
            return View(fix_show.ToList());
        }

        // GET: b_Fix_Point/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Fix_Point b_Fix_Point = db.b_Fix_Point.Find(id);
            if (b_Fix_Point == null)
            {
                return HttpNotFound();
            }
            return View(b_Fix_Point);
        }

        // GET: b_Fix_Point/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Fix_Point/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Fix_Point_Type,b_Fix_Point_Name, b_Fix_Point_Value,b_Create_User_ID,b_Note,Create_Time")] b_Fix_Point b_Fix_Point)
        {
            if (ModelState.IsValid)
            {
                b_Fix_Point.Create_Time = DateTime.Now;
                b_Fix_Point.b_Create_User_ID = (Session["User"] as b_User).ID;
                b_Fix_Point.b_Enterprise = Session["Enterprise_id"].ToString();
                db.b_Fix_Point.Add(b_Fix_Point);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Fix_Point);
        }

        // GET: b_Fix_Point/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Fix_Point b_Fix_Point = db.b_Fix_Point.Find(id);
            if (b_Fix_Point == null)
            {
                return HttpNotFound();
            }
            return View(b_Fix_Point);
        }

        // POST: b_Fix_Point/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Fix_Point_Type,b_Fix_Point_Name,b_Fix_Point_Value,b_Create_User_ID,b_Note,Create_Time,b_Enterprise")] b_Fix_Point b_Fix_Point)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Fix_Point).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Fix_Point);
        }

        // GET: b_Fix_Point/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Fix_Point b_Fix_Point = db.b_Fix_Point.Find(id);
            if (b_Fix_Point == null)
            {
                return HttpNotFound();
            }
            return View(b_Fix_Point);
        }

        // POST: b_Fix_Point/Delete/5
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Fix_Point b_Fix_Point = db.b_Fix_Point.Find(id);
            
            if (b_Fix_Point == null)
            {
                return HttpNotFound();
            }
            var fix_point_user = from usr in db.b_Fix_Point_To_User
                                 where usr.b_Fix_Point_ID == b_Fix_Point.ID
                                 select usr;
            db.b_Fix_Point_To_User.RemoveRange(fix_point_user);
            db.b_Fix_Point.Remove(b_Fix_Point);
            
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
