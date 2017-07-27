using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using Bounds.Utils;

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_WorkAgeController : Controller
    {
        private BoundsContext db = new BoundsContext();
        private void SetViewBag(int? id)
        {
            if (id == null) id = 0;
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "按月结算", Value = "0", Selected = (id == 0) },
                new SelectListItem() { Text = "按天结算", Value = "1", Selected = (id == 1) }
            };
            ViewBag.BalanceType = list;
        }
        // GET: b_WorkAge
        public ActionResult Index()
        {
            string strEnterprise = Session["Enterprise_id"].ToString();
            var workage = db.b_WorkAge.Where(x => x.b_Enterprise == strEnterprise).FirstOrDefault();
            SetViewBag((workage == null) ? null : (int?)workage.b_Balance_Type);
            return View(workage);
        }

        // GET: b_WorkAge/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: b_WorkAge/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_WorkAge/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: b_WorkAge/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: b_WorkAge/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                int id = Request.Form["ID"].to_i();
                int balance_type = Request.Form["b_Balance_Type"].to_i();
                int point_value = Request.Form["b_Point_Value"].to_i();
                string end_date = Request.Form["b_End_Date"].ToString();
                string enterprise = Request.Form["b_Enterprise"].ToString();
                string create_time = Request.Form["Create_Time"].ToString();

                var workage = (from wa in db.b_WorkAge
                              where wa.ID == id
                              select wa).FirstOrDefault();
                if (workage == null)
                {
                    workage = new b_WorkAge();
                    workage.b_Balance_Type = balance_type;
                    workage.b_Point_Value = point_value;
                    workage.b_End_Date = end_date;
                    workage.Create_Time = DateTime.Now;
                    workage.Update_Time = DateTime.Now;
                    workage.b_Enterprise = Session["Enterprise_id"].ToString();

                    db.b_WorkAge.Add(workage);
                }
                else
                {
                    workage.b_Balance_Type = balance_type;
                    workage.b_Point_Value = point_value;
                    workage.b_End_Date = end_date;
                    workage.Create_Time = Convert.ToDateTime(create_time);
                    workage.Update_Time = DateTime.Now;
                    workage.b_Enterprise = enterprise;
                }
                db.SaveChanges();
                SetViewBag((workage == null) ? null : (int?)workage.b_Balance_Type);
                ViewBag.Body = "保存成功。";
                return View("Index", workage);
            }
            catch (Exception ex)
            {
                ViewBag.Body = ex.Message;
                Log.logger.Error("保存工龄分配置出现错误：" + ex.Message);
                return View("Index");
            }
        }

        // GET: b_WorkAge/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: b_WorkAge/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
