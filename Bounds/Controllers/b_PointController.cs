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
using System.Transactions;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_PointController : Controller
    {
        private BoundsContext db = new BoundsContext();

        private void SetViewBag()
        {
            int ent_id = Convert.ToInt32(Session["Enterprise_id"]);
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
            string strEnterprise = Session["Enterprise_id"].ToString();
            int user_id = (Session["User"] as b_User).ID;
            string strSQLSel = @"select a.*, b.b_RealName as b_First_Check_Name, c.b_RealName as b_Final_Check_Name, d.b_RealName as b_Recorder_Name from b_Point as a 
                                join b_User as b on a.b_First_Check_ID = b.ID
                                join b_User as c on a.b_Final_Check_ID = c.ID
                                join b_User as d on a.b_Recorder_ID = d.ID
                                where a.b_Recorder_ID = " + user_id + " and a.b_Enterprise='" + strEnterprise + "'";
            IEnumerable<b_Point_Record> record = db.Database.SqlQuery<b_Point_Record>(strSQLSel);
            return View(record.ToList());
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

            foreach (var point_event in b_Point.b_Point_Event)
            {
                foreach (var member in point_event.b_Point_Event_Member)
                {
                    string strUserName = db.b_User.Where(x => x.ID == member.b_User_ID).FirstOrDefault().b_UserName;
                    
                }
            }

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
                    using (TransactionScope ts = new TransactionScope())
                    {
                        b_Point.Create_Time = DateTime.Now;
                        b_Point.Update_Time = DateTime.Now;
                        b_Point.b_Record_Time = DateTime.Now;
                        b_Point.b_Enterprise = Session["Enterprise_id"].ToString();
                        b_Point.b_Recorder_ID = ((b_User)Session["User"]).ID;
                        b_Point.TheMonth = b_Point.b_Event_Date.ToString("yyyyMM");
                        db.b_Point.Add(b_Point);
                        db.SaveChanges();
                        //功分明细表插入
                        foreach (var point_event in b_Point.b_Point_Event)
                        {
                            foreach (var member in point_event.b_Point_Event_Member)
                            {
                                b_Point_Details detail = new b_Point_Details();
                                detail.b_Enterprise = Session["Enterprise_id"].ToString();
                                detail.b_Event_ID = point_event.ID;
                                detail.b_Point_Type = 1;
                                detail.b_Point_Value = member.b_A_Point;
                                detail.b_User_ID = member.b_User_ID;
                                detail.Create_Time = DateTime.Now;
                                detail.Update_Time = DateTime.Now;
                                detail.b_Recorder_ID = b_Point.b_Recorder_ID;
                                detail.TheMonth = DateTime.Now.ToString("yyyyMM");
                                db.b_Point_Details.Add(detail);
                                db.SaveChanges();
                                detail.b_Point_Type = 2;
                                detail.b_Point_Value = member.b_B_Point;
                                db.b_Point_Details.Add(detail);
                                db.SaveChanges();
                                detail.b_Point_Type = (int)member.b_Value_Type + 3;
                                detail.b_Point_Value = member.b_Value_Point;
                                db.b_Point_Details.Add(detail);
                                db.SaveChanges();
                            }
                        }
                        //db.SaveChanges();
                        ts.Complete();
                    }
                    return Json("OK");
                }
                return Json("数据模型不可用");
            }
            catch(Exception ex)
            {
                Log.logger.Error("保存功分奖扣时出现错误：" + ex.Message);
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
                //获取我的功分0：固定功分，1：奖扣功分，2：其他得分
                int ent_id = Session["Enterprise_id"].to_i();
                int user_id = (Session["User"] as b_User).ID;
                //获取固定功分
                string strSQLGetFixPoint = "select b.b_Fix_Point_Name, b.b_Fix_Point_Value from b_Fix_Point_To_User as a join b_Fix_Point as b on a.b_Fix_Point_ID = b.ID where a.b_User_id =" + user_id;
                var my_fix_point = db.Database.SqlQuery<My_Fix_Point_Model>(strSQLGetFixPoint);
                ViewBag.Fix_Point = my_fix_point;
                //获取奖扣功分
                string strSQLGetPointEvent= @"select a.b_Event_Date,e.b_Event_Name,c.b_A_Point,c.b_B_Point, f.b_RealName as b_First_Check_Name,g.b_RealName as b_Final_Check_Name 
                                            from b_Point as a inner join b_Point_Event as b on a.ID = b.b_Point_ID 
                                            inner join b_Event_Library as e on b.b_Event_ID = e.ID 
                                            inner join b_Point_Event_Member as c on b.ID = c.b_Point_Event_ID 
                                            inner join b_User as f on a.b_First_Check_ID = f.ID 
                                            inner join b_User as g on a.b_Final_Check_ID = g.ID 
                                            where a.b_Enterprise = " + ent_id + " and c.b_User_ID = " + user_id;
                var my_point_event = db.Database.SqlQuery<My_Point_Event_Model>(strSQLGetPointEvent);
                ViewBag.Point_Event = my_point_event;
                //获取其他得分，启动分和工龄分
                var workage = (from w in db.b_WorkAge
                              where w.b_Enterprise == ent_id.ToString()
                              select w).FirstOrDefault();
                var entry_date = ((b_User)Session["User"]).b_EntryDate;
                var start_point = (from s in db.b_StartPoint
                                   where s.b_Enterprise == ent_id.ToString()
                                   select s).FirstOrDefault();
                
                double nGongLing = 0;
                if (entry_date != null)
                {
                    TimeSpan ts = DateTime.Now - Convert.ToDateTime(entry_date);
                    if (workage.b_Balance_Type == 0)//按月结算
                    {
                        nGongLing = ts.Days / 30 * workage.b_Point_Value;
                    }
                    else//按天结算
                    {
                        nGongLing = ts.Days * workage.b_Point_Value;
                    }
                }

                string strSQLGetOthers = @"select '启动分' as b_Other_Name, " + start_point.b_StartPoint_Value + " as b_Other_Point union select '工龄分' as b_Other_Name, " + (Int32)nGongLing + " as b_Other_Point";
                var my_point_others = db.Database.SqlQuery<My_Point_Others_Model>(strSQLGetOthers);
                ViewBag.Others = my_point_others;
                return View();
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取我的功分时出现错误：" + ex.Message);
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
                Log.logger.Error("获取我的功分时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult My_Check()
        {
            try
            {
                IEnumerable<Point_Record_Model> my_check = GetMyCheck();
                if (my_check == null)
                {
                    return View();
                }
                else
                {
                    return View(my_check);
                }
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取我的审核数据时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private IEnumerable<Point_Record_Model> GetMyCheck()
        {
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
            if (my_check_value.Contains(2))//如果是终审人
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
                string strSQLGetRecords = @"select a.ID,a.b_Event_Date, a.b_Record_Time, a.b_Subject, Convert(varchar(10),Sum(f.b_A_Point)) as b_A_Point, Convert(varchar(10),Sum(f.b_B_Point)) as b_B_Point, Convert(varchar(10),Sum(f.b_Value_Point)) as b_Value_Point, Count(f.b_User_ID) as b_PeopleCount_Value,b.b_RealName as b_First_Check_Name, c.b_RealName as b_Final_Check_Name, d.b_RealName as b_Recorder_Name, a.b_Status
                        from b_Point as a join b_User as b on a.b_First_Check_ID = b.ID
                        join b_User as c on a.b_Final_Check_ID = c.ID
                        join b_User as d on a.b_Recorder_ID = d.ID
                        join b_Point_Event as e on e.b_Point_ID = a.ID
                        join b_Point_Event_Member as f on f.b_Point_Event_ID = e.ID
                        Where " + sb.ToString() + " Group by a.ID, a.b_Event_Date, a.b_Record_Time, a.b_Subject,b.b_RealName,c.b_RealName, d.b_RealName,a.b_Status";
                IEnumerable<Point_Record_Model> record = db.Database.SqlQuery<Point_Record_Model>(strSQLGetRecords);
                return record;
            }
            return null;
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
                if(point.b_Final_Check_ID.to_i() == cur_user_id)
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

        public ActionResult My_Detail()
        {
            try
            {
                int id = (Session["User"] as b_User).ID;
                string strSQLSel = @"select b.b_Point_Type_Name,a.b_Point_Value,c.b_Event_Note as b_Event_Name,a.Create_Time From
                                     b_Point_Details as a join b_Point_Type_Dic as b on a.b_Point_Type=b.ID
                                     join b_Point_Event as c on a.b_Event_ID = c.ID
                                     where a.b_User_ID = " + id;

                IEnumerable<b_Point_Details_ShowInfo> my_detail = db.Database.SqlQuery<b_Point_Details_ShowInfo>(strSQLSel);

                return View(my_detail);
            }
            catch(Exception ex)
            {
                Log.logger.Error("进行个人明细查询时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Usual_Point()
        {
            try
            {
                string strEnterprise = Session["Enterprise_id"].ToString();
                string strSQLSel = @"select a.*, b.b_RealName as b_First_Check_Name, c.b_RealName as b_Final_Check_Name, d.b_RealName as b_Recorder_Name from b_Point as a 
                                join b_User as b on a.b_First_Check_ID = b.ID
                                join b_User as c on a.b_Final_Check_ID = c.ID
                                join b_User as d on a.b_Recorder_ID = d.ID
                                where a.b_Enterprise='" + strEnterprise + "'";
                IEnumerable<b_Point_Record> record = db.Database.SqlQuery<b_Point_Record>(strSQLSel);
                return View(record.ToList());
            }
            catch(Exception ex)
            {
                Log.logger.Error("获取日常奖扣查询数据时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Point_Check()
        {
            try
            {
                string strSQLSel = @"select d.b_RealName as b_Point_Object, c.b_Event_Date, c.b_Record_Time, c.b_Subject, e.b_Event_Name, Convert(varchar(255),a.b_A_Point) as b_A_Point, Convert(varchar(255),a.b_B_Point) as b_B_Point, Convert(varchar(255),a.b_Value_Point) as b_Value_Point, f.b_RealName as b_First_Check_Name, g.b_RealName as b_Final_Check_Name, h.b_RealName as b_Recorder_Name, c.b_Status 
                                     from b_Point_Event_Member as a 
                                     join b_Point_Event as b on a.b_Point_Event_ID = b.id 
                                     join b_Point as c on b.b_Point_ID = c.ID
                                     join b_User as d on a.b_User_ID = d.ID
                                     join b_Event_Library as e on b.b_Event_ID = e.ID
                                     join b_User as f on c.b_First_Check_ID = f.ID
                                     join b_User as g on c.b_Final_Check_ID = g.ID
                                     join b_User as h on c.b_Recorder_ID = h.ID
                                     Where c.b_Enterprise = '" + Session["Enterprise_id"].ToString() + "'";
                IEnumerable<Point_Check_Model> point_check = db.Database.SqlQuery<Point_Check_Model>(strSQLSel);

                return View(point_check);
            }
            catch (Exception ex)
            {
                Log.logger.Error("功分核查载入时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }

        public ActionResult Value_Check()
        {
            try
            {
                string strSQLSel = @"select c.TheMonth, d.b_RealName, d.b_WorkNum, Convert(varchar(255),Sum(case when a.b_Value_Type='0' then a.b_Value_Point else 0 end)) as b_C_Value, Convert(varchar(255),Sum(case when a.b_Value_Type='1' then a.b_Value_Point else 0 end)) as b_S_Value,Convert(varchar(255),Sum(case when a.b_Value_Type='2' then a.b_Value_Point else 0 end)) as b_X_Value,Convert(varchar(255),Sum(a.b_Value_Point)) as b_Total_Value
                                    from b_Point_Event_Member as a 
                                    join b_Point_Event as b on a.b_Point_Event_ID = b.id 
                                    join b_Point as c on b.b_Point_ID = c.ID
                                    join b_User as d on a.b_User_ID = d.ID
                                    Where c.b_Enterprise = '" + Session["Enterprise_id"].ToString() + "' Group by c.TheMonth,d.b_RealName,d.b_WorkNum";

                IEnumerable<Value_Check_Model> value_check = db.Database.SqlQuery<Value_Check_Model>(strSQLSel);
                return View(value_check);
            }
            catch (Exception ex)
            {
                Log.logger.Error("产值核查载入时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }

        //产值排名报表
        public ActionResult Value_Order_Report()
        {
            try
            {
                string strEnterprise = Session["Enterprise_id"].ToString();
                string strSQLSel = @"select Row_Number() over(order by Sum(a.b_Value_Point) DESC) as ID, c.TheMonth, d.b_RealName, d.b_WorkNum, Convert(varchar(255),Sum(case when a.b_Value_Type='0' then a.b_Value_Point else 0 end)) as b_C_Value, Convert(varchar(255),Sum(case when a.b_Value_Type='1' then a.b_Value_Point else 0 end)) as b_S_Value,Convert(varchar(255),Sum(case when a.b_Value_Type='2' then a.b_Value_Point else 0 end)) as b_X_Value,Convert(varchar(255),Sum(a.b_Value_Point)) as b_Total_Value
                                    from b_Point_Event_Member as a 
                                    join b_Point_Event as b on a.b_Point_Event_ID = b.id 
                                    join b_Point as c on b.b_Point_ID = c.ID
                                    join b_User as d on a.b_User_ID = d.ID
                                    Where c.b_Enterprise = '" + Session["Enterprise_id"].ToString() + "' Group by c.TheMonth,d.b_RealName,d.b_WorkNum";
                IEnumerable<Value_Order_Report> value_order_report = db.Database.SqlQuery<Value_Order_Report>(strSQLSel);
                return View(value_order_report);
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取产值排名时出现错误：" + ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //管理人员奖罚分报表
        public ActionResult Manager_Reward_Report()
        {
            try
            {
                string strSQLSel = @"select d.b_RealName as b_Point_Object, c.b_Event_Date, c.b_Record_Time, c.b_Subject, e.b_Event_Name, Convert(varchar(255),Sum(case when a.b_A_Point>0 then a.b_A_Point else 0 end) + Sum(case when a.b_B_Point>0 then a.b_B_Point else 0 end)) as b_Total_Point, h.b_RealName as b_Recorder_Name
                                     from b_Point_Event_Member as a 
                                     join b_Point_Event as b on a.b_Point_Event_ID = b.id 
                                     join b_Point as c on b.b_Point_ID = c.ID
                                     join b_User as d on a.b_User_ID = d.ID
                                     join b_Event_Library as e on b.b_Event_ID = e.ID
                                     join b_User as f on c.b_First_Check_ID = f.ID
                                     join b_User as g on c.b_Final_Check_ID = g.ID
                                     join b_User as h on c.b_Recorder_ID = h.ID
                                     Where c.b_Enterprise = '" + Session["Enterprise_id"].ToString() + "'";
                return View();
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取管理人员奖罚分报表时出现错误：" + ex.Message);
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
