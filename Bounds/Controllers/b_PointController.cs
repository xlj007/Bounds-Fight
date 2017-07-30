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
    public class b_PointController : Controller
    {
        private BoundsContext db = new BoundsContext();

        private void SetViewBag()
        {
            int ent_id = Convert.ToInt16(Session["Enterprise_id"]);
            var list_first_check = from u in db.b_User
                                   join cu in db.b_Check_User on u.ID equals cu.b_User_ID
                                   join ent in db.b_Organize on cu.b_Organize_ID equals ent.ID
                                   where ent.b_Enterprise_Id == ent_id && u.b_Enterprise_ID == ent_id && cu.b_Check_Type == 1
                                   select u;
            var list_final_check = from u in db.b_User
                                   join cu in db.b_Check_User on u.ID equals cu.b_User_ID
                                   join ent in db.b_Organize on cu.b_Organize_ID equals ent.ID
                                   where ent.b_Enterprise_Id == ent_id && u.b_Enterprise_ID == ent_id && cu.b_Check_Type == 2
                                   select u;

            ViewBag.First_Check = list_first_check.ToList();
            ViewBag.Final_Check = list_final_check.ToList();
        }
        // GET: b_Point
        public ActionResult Index()
        {
            return View(db.b_Point.ToList());
        }

        // GET: b_Point/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Point b_Point = db.b_Point.Find(id);
            if (b_Point == null)
            {
                return HttpNotFound();
            }
            return View(b_Point);
        }

        // GET: b_Point/Create
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: b_Point/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Event_Date,b_Record_Time,b_Subject,b_Note,b_First_Note,b_Final_Note,b_Status,b_Enterprise,Create_Time,Update_Time")] b_Point b_Point)
        {
            if (ModelState.IsValid)
            {
                db.b_Point.Add(b_Point);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Point);
        }

        // GET: b_Point/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Point b_Point = db.b_Point.Find(id);
            if (b_Point == null)
            {
                return HttpNotFound();
            }
            return View(b_Point);
        }

        // POST: b_Point/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Event_Date,b_Record_Time,b_Subject,b_Note,b_First_Note,b_Final_Note,b_Status,b_Enterprise,Create_Time,Update_Time")] b_Point b_Point)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Point).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Point);
        }

        [HttpPost]
        public ActionResult Save([Bind(Include = "ID,b_Event_Date,b_Record_Time,b_Subject,b_Note,b_First_Note,b_Final_Note,b_Status,b_Point_Event,b_Enterprise,b_First_Check_ID, b_Final_Check_ID,b_Recorder_ID,Create_Time,Update_Time")] b_Point b_Point)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    b_Point.Create_Time = DateTime.Now;
                    b_Point.Update_Time = DateTime.Now;
                    b_Point.b_Record_Time = DateTime.Now;
                    b_Point.b_Enterprise = Session["Enterprise_id"].ToString();
                    b_Point.b_Recorder_ID = ((b_User)Session["User"]).ID;
                    db.b_Point.Add(b_Point);
                    db.SaveChanges();
                    return Json("OK");
                }
                return Json("数据模型不可用");
            }
            catch(Exception ex)
            {
                Log.logger.Error("保存积分奖扣时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }

        // GET: b_Point/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Point b_Point = db.b_Point.Find(id);
            if (b_Point == null)
            {
                return HttpNotFound();
            }
            return View(b_Point);
        }

        // POST: b_Point/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Point b_Point = db.b_Point.Find(id);
            db.b_Point.Remove(b_Point);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult My_Points(int nType = 0)
        {
            try
            {
                //获取我的积分0：固定积分，1：奖扣积分，2：其他得分
                //var 
                return Json("OK");
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取我的积分时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        public ActionResult My_Values(int? id)
        {
            try
            {
                if (id == null) id = 0;
                //获取我的产值0：创富产值，1：实产值，2：虚产值
                string strSQLSel = @"select a.b_Event_Date,e.b_Event_Name,c.b_Value_Point, f.b_RealName as FirstCheckName,g.b_RealName as FinalCheckName from b_Point as a inner join b_Point_Event as b on a.ID = b.b_Point_ID inner join b_Event_Library as e on b.b_Event_ID = e.ID inner join b_Point_Event_Member as c on b.ID = c.b_Point_Event_ID inner join b_User as f on a.b_First_Check_ID = f.ID inner join b_User as g on a.b_Final_Check_ID = g.ID where a.b_Enterprise = '" + Session["Enterprise_id"].ToString() + "' and c.b_User_ID = " + (Session["User"] as b_User).ID + " and c.b_Value_Type = " + id;
                //var my_values = db.Database.ExecuteSqlCommand(strSQLSel);
                var my_values = db.Database.SqlQuery<My_Values_Model>(strSQLSel).ToList();
                ViewBag.myValue = my_values;
                ViewData["RouteValue"] = id;
                return View();
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取我的积分时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
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
