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
using System.Text;

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
            b_Point b_Point = (from point in db.b_Point.Include("b_Point_Event").Include("b_Point_Event.b_Point_Event_Member")
                               where point.ID == id
                               select point).FirstOrDefault();

            if (b_Point == null)
            {
                return HttpNotFound();
            }

            return View(b_Point);
        }

        public string Modify(string pk, string name, string value)
        {
            string strSQLUpdate = "Update b_Point_Event_Member Set " + name + "=" + value + " where ID =" + pk;
            db.Database.ExecuteSqlCommand(strSQLUpdate);
            return string.Empty;
        }

        public ActionResult PointEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Point b_Point = (from point in db.b_Point.Include("b_Point_Event").Include("b_Point_Event.b_Point_Event_Member")
                               where point.ID == id
                               select point).FirstOrDefault();

            if (b_Point == null)
            {
                return HttpNotFound();
            }
            SetViewBag();
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
        public ActionResult DeleteConfirmed(int id)
        {
            b_Point b_Point = (from point in db.b_Point.Include("b_Point_Event").Include("b_Point_Event.b_Point_Event_Member")
                               where point.ID == id
                               select point).FirstOrDefault();
            db.b_Point.Remove(b_Point);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult My_Points()
        {
            try
            {
                //获取我的积分0：固定积分，1：奖扣积分，2：其他得分
                int ent_id = Session["Enterprise_id"].to_i();
                int user_id = (Session["User"] as b_User).ID;
                //获取固定积分
                string strSQLGetFixPoint = "select b.b_Fix_Point_Name, b.b_Fix_Point_Value from b_Fix_Point_To_User as a join b_Fix_Point as b on a.b_Fix_Point_ID = b.ID where a.b_User_id =" + user_id;
                var my_fix_point = db.Database.SqlQuery<My_Fix_Point_Model>(strSQLGetFixPoint);
                ViewBag.Fix_Point = my_fix_point;
                //获取奖扣积分
                string strSQLGetPointEvent= @"select a.b_Event_Date,e.b_Event_Name,c.b_A_Point,c.b_B_Point, f.b_RealName as b_First_Check_Name,g.b_RealName as b_Final_Check_Name 
                                            from b_Point as a inner join b_Point_Event as b on a.ID = b.b_Point_ID 
                                            inner join b_Event_Library as e on b.b_Event_ID = e.ID 
                                            inner join b_Point_Event_Member as c on b.ID = c.b_Point_Event_ID 
                                            inner join b_User as f on a.b_First_Check_ID = f.ID 
                                            inner join b_User as g on a.b_Final_Check_ID = g.ID 
                                            where a.b_Enterprise = " + ent_id + " and c.b_User_ID = " + user_id;
                var my_point_event = db.Database.SqlQuery<My_Point_Event_Model>(strSQLGetPointEvent);
                ViewBag.Point_Event = my_point_event;
                //获取其他得分
                return View();
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取我的积分时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult My_Values(int? id)
        {
            try
            {
                if (id == null) id = 0;
                //获取我的产值0：创富产值，1：实产值，2：虚产值
                string strSQLSel = @"select a.b_Event_Date,e.b_Event_Name,c.b_Value_Point, f.b_RealName as FirstCheckName,g.b_RealName as FinalCheckName 
                                    from b_Point as a inner join b_Point_Event as b on a.ID = b.b_Point_ID 
                                    inner join b_Event_Library as e on b.b_Event_ID = e.ID 
                                    inner join b_Point_Event_Member as c on b.ID = c.b_Point_Event_ID 
                                    inner join b_User as f on a.b_First_Check_ID = f.ID 
                                    inner join b_User as g on a.b_Final_Check_ID = g.ID 
                                    where a.b_Enterprise = '" + Session["Enterprise_id"].ToString() + "' and c.b_User_ID = " + (Session["User"] as b_User).ID + " and c.b_Value_Type = " + id;
                var my_values = db.Database.SqlQuery<My_Values_Model>(strSQLSel).ToList();
                ViewBag.myValue = my_values;
                return View();
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取我的积分时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult My_Check()
        {
            try
            {
                //获取我的审查权限
                int user_id = (Session["User"] as b_User).ID;
                var my_check_value = (from cu in db.b_Check_User
                                     join u in db.b_User on cu.b_User_ID equals u.ID
                                     where u.ID == user_id
                                     select cu).Select(x => x.b_Check_Type).Distinct().ToList();

                StringBuilder sb = new StringBuilder();
                if (my_check_value.Contains(1))//如果是初审人
                {
                    sb.Append("(a.b_Status = 0 and a.b_First_Check_ID = ");
                    sb.Append(user_id);
                    sb.Append(")");
                }
                else if (my_check_value.Contains(2))//如果是终审人
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" or ");
                    }
                    sb.Append("(a.b_Status = 2 and a.b_Final_Check_ID = ");
                    sb.Append(user_id);
                    sb.Append(")");
                }
                //组SQL
                if (sb.Length > 0)
                {
                    string strSQLGetRecords = @"select a.ID,a.b_Event_Date, a.b_Record_Time, a.b_Subject, Convert(varchar(10),Sum(f.b_A_Point)) as b_A_Point, Convert(varchar(10),Sum(f.b_B_Point)) as b_B_Point, Convert(varchar(10),Sum(f.b_Value_Point)) as b_Value_Point, Count(f.b_User_ID) as b_PeopleCount_Value,b.b_RealName as b_First_Check_Name, c.b_RealName as b_Final_Check_Name, d.b_RealName as b_Recorder_Name 
                        from b_Point as a join b_User as b on a.b_First_Check_ID = b.ID
                        join b_User as c on a.b_Final_Check_ID = c.ID
                        join b_User as d on a.b_Recorder_ID = d.ID
                        join b_Point_Event as e on e.b_Point_ID = a.ID
                        join b_Point_Event_Member as f on f.b_Point_Event_ID = e.ID
                        Where " + sb.ToString() + " Group by a.ID, a.b_Event_Date, a.b_Record_Time, a.b_Subject,b.b_RealName,c.b_RealName, d.b_RealName";
                    IEnumerable<Point_Record_Model> record = db.Database.SqlQuery<Point_Record_Model>(strSQLGetRecords);
                    return View(record);
                }
                return View();
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取我的审核数据时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Comment(string strMsg, string strType, int? nPoint_id)
        {
            if (nPoint_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var point = db.b_Point.Find(nPoint_id);
                int cur_user_id = (Session["User"] as b_User).ID;
                if(point.b_First_Check_ID.to_i() == cur_user_id)
                {
                    point.b_First_Note = strMsg;
                    if (strType == "pass")
                    {
                        point.b_Status = b_Status.待终审;
                    }
                    else
                    {
                        point.b_Status = b_Status.初审驳回;
                    }
                }
                else if(point.b_Final_Check_ID.to_i() == cur_user_id)
                {
                    point.b_Final_Note = strMsg;
                    if (strType == "pass")
                    {
                        point.b_Status = b_Status.审核通过;
                    }
                    else
                    {
                        point.b_Status = b_Status.终审驳回;

                    }
                }
                db.SaveChanges();
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存评论时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Back(int point_id)
        {
            try
            {
                var point = db.b_Point.Find(point_id);
                int cur_user_id = (Session["User"] as b_User).ID;
                if (point.b_Recorder_ID.to_i() == cur_user_id)
                {
                    point.b_Status = b_Status.撤回;
                }
 
                db.SaveChanges();
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存评论时出现错误：" + ex.Message);
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
