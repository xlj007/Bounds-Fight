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
    public class b_StartPointController : Controller
    {
        private BoundsContext db = new BoundsContext();
        // GET: b_StartPoint
        public ActionResult Index()
        {
            string strEnterprise = Session["Enterprise_id"].ToString();
            var start_point = db.b_StartPoint.Where(x => x.b_Enterprise == strEnterprise).FirstOrDefault();
            return View(start_point);
        }

        // GET: b_StartPoint/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: b_StartPoint/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_StartPoint/Create
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

        // GET: b_StartPoint/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: b_StartPoint/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                int id = Request.Form["ID"].to_i();
                int start_point_value = Request.Form["b_StartPoint_Value"].to_i();
                string enterprise = Request.Form["b_Enterprise"].ToString();
                string create_time = Request.Form["Create_Time"].ToString();

                var start_point = (from sp in db.b_StartPoint
                                   where sp.ID == id
                                   select sp).FirstOrDefault();
                if (start_point == null)
                {
                    start_point = new b_StartPoint();
                    start_point.b_StartPoint_Value = start_point_value;
                    start_point.b_Enterprise = Session["Enterprise_id"].ToString();
                    start_point.Create_Time = DateTime.Now;
                    start_point.Update_Time = DateTime.Now;
                    db.b_StartPoint.Add(start_point);
                }
                else
                {
                    start_point.b_StartPoint_Value = start_point_value;
                    start_point.b_Enterprise = enterprise;
                    start_point.Create_Time = Convert.ToDateTime(create_time);
                    start_point.Update_Time = DateTime.Now;
                }
                db.SaveChanges();
                ViewBag.Body = "保存成功。";
                return View("Index", start_point);
            }
            catch(Exception ex)
            {
                ViewBag.Body = ex.Message;
                Log.logger.Error("保存启动分错误：" + ex.Message);
                return View("Index");
            }
        }

        // GET: b_StartPoint/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: b_StartPoint/Delete/5
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
