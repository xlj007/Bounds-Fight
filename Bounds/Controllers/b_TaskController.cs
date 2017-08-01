using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using System.Transactions;
using Bounds.Utils;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_TaskController : Controller
    {
        private BoundsContext db = new BoundsContext();
        
        [HttpPost]
        public ActionResult GetTaskUser(int task_id)
        {
            try
            {
                var user = from u in db.b_User
                           join tu in db.b_Task_To_User on u.ID equals tu.b_User_ID
                           where tu.b_Task_ID == task_id
                           select new { u.ID, u.b_UserName };

                return Json(user.ToList());
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取奖扣任务关联人员时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SaveTaskUser(int task_id, string user_id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var cur_user = from task_info in db.b_Task_To_User
                                   where task_info.b_Task_ID == task_id
                                   select task_info;
                    db.b_Task_To_User.RemoveRange(cur_user);

                    string[] arrUserIds = user_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strUserId in arrUserIds)
                    {
                        b_Task_To_User b_task_to_user = new b_Task_To_User();
                        b_task_to_user.b_Task_ID = task_id;
                        b_task_to_user.b_User_ID = strUserId.to_i();
                        db.b_Task_To_User.Add(b_task_to_user);
                    }
                    db.SaveChanges();
                    ts.Complete();
                }
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存奖扣任务关联人员时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        // GET: b_Task
        public ActionResult Index()
        {
            string strEnterprise = Session["Enterprise_id"].ToString();
            return View(db.b_Task.Where(x=>x.b_Enterprise == strEnterprise).ToList());
        }

        // GET: b_Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task b_Task = db.b_Task.Find(id);
            if (b_Task == null)
            {
                return HttpNotFound();
            }
            return View(b_Task);
        }

        // GET: b_Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Task/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Task_Name")] b_Task b_Task)
        {
            if (ModelState.IsValid)
            {
                b_Task.b_Enterprise = Session["Enterprise_id"].ToString();
                b_Task.Create_Time = DateTime.Now;
                b_Task.Update_Time = DateTime.Now;
                db.b_Task.Add(b_Task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Task);
        }

        // GET: b_Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task b_Task = db.b_Task.Find(id);
            if (b_Task == null)
            {
                return HttpNotFound();
            }
            return View(b_Task);
        }

        // POST: b_Task/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Task_Name,b_Enterprise,Create_Time")] b_Task b_Task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Task).State = EntityState.Modified;
                b_Task.Update_Time = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Task);
        }

        // GET: b_Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task b_Task = db.b_Task.Find(id);
            if (b_Task == null)
            {
                return HttpNotFound();
            }
            return View(b_Task);
        }

        // POST: b_Task/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Task b_Task = db.b_Task.Find(id);
            if (b_Task == null)
            {
                return HttpNotFound();
            }
            db.b_Task.Remove(b_Task);
            db.b_Task_To_User.RemoveRange(db.b_Task_To_User.Where(x=>x.b_Task_ID == id));
            db.b_Task_Info.RemoveRange(db.b_Task_Info.Where(x => x.b_Task_ID == b_Task.ID));
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
