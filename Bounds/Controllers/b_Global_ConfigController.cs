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
    public class b_Global_ConfigController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Global_Config
        public ActionResult Index()
        {
            int ent_id = Session["Enterprise_id"].to_i();
            var config = (from g in db.b_Global_Config.Include("b_Global_Config_Item")
                                     where g.b_Enterprise_ID == ent_id
                                     select g).FirstOrDefault();
            

            //if (config != null)
            //{
            //    config.ChangeToEdit(config).b_Global_Config_Item = (from item in db.b_Global_Config_Item
            //                                   where item.b_Global_Config_ID == config.ID
            //                                   orderby item.b_Item_Type
            //                                   select item).ToList();
            //}
            return View(config);
        }

        // GET: b_Global_Config/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            if (b_Global_Config == null)
            {
                return HttpNotFound();
            }
            return View(b_Global_Config);
        }

        // GET: b_Global_Config/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "ID,b_Enterprise_ID,b_Recorder_Add,b_Recorder_Price,b_ChuangFu_To_Bounds,b_ActualValue_To_Bounds,b_VirtualValue_To_Bounds,b_Sale_To_Bounds,b_Attence_To_Bounds,b_A_To_B,b_Price_Paper_Set,b_SignIn_Bounds,b_SignIn_Time,b_FixedBounds_ToAttence,b_Check_Date,b_Global_Config_Item")] b_Global_Config b_Global_Config)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var config_item in b_Global_Config.b_Global_Config_Item)
                    {
                        config_item.Create_Time = DateTime.Now;
                        config_item.Update_Time = DateTime.Now;
                    }

                    b_Global_Config.Update_Time = DateTime.Now;
                    b_Global_Config.b_Enterprise_ID = Session["Enterprise_id"].to_i();
                    b_Global_Config.Create_Time = DateTime.Now;

                    if (b_Global_Config.ID == 0)
                    {
                        db.b_Global_Config.Add(b_Global_Config);
                    }
                    else
                    {
                        var cur_item = db.b_Global_Config.Include("b_Global_Config_Item").Where(x => x.ID == b_Global_Config.ID).FirstOrDefault();
                        db.b_Global_Config_Item.RemoveRange(cur_item.b_Global_Config_Item);
                        cur_item.b_Global_Config_Item = b_Global_Config.b_Global_Config_Item;
                        db.Entry(cur_item).CurrentValues.SetValues(b_Global_Config);
                    }
                    db.SaveChanges();
                    ts.Complete();
                }
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存全局设置时出现问题：" + Environment.NewLine + ex.Message);
                return Json(ex.Message);
            }
        }
        // POST: b_Global_Config/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_Enterprise_ID,b_Recorder_Add,b_Recorder_Price,b_ChuangFu_To_Bounds,b_ActualValue_To_Bounds,b_VirtualValue_To_Bounds,b_Sale_To_Bounds,b_Attence_To_Bounds,b_A_To_B,b_Price_Paper_Set,b_SignIn_Bounds,b_SignIn_Time,b_FixedBounds_ToAttence,b_Check_Date")] b_Global_Config b_Global_Config)
        {
            if (ModelState.IsValid)
            {
                db.b_Global_Config.Add(b_Global_Config);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Global_Config);
        }

        // GET: b_Global_Config/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            if (b_Global_Config == null)
            {
                return HttpNotFound();
            }
            return View(b_Global_Config);
        }

        // POST: b_Global_Config/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_Enterprise_ID,b_Recorder_Add,b_Recorder_Price,b_ChuangFu_To_Bounds,b_ActualValue_To_Bounds,b_VirtualValue_To_Bounds,b_Sale_To_Bounds,b_Attence_To_Bounds,b_A_To_B,b_Price_Paper_Set,b_SignIn_Bounds,b_SignIn_Time,b_FixedBounds_ToAttence,b_Check_Date")] b_Global_Config b_Global_Config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Global_Config).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Global_Config);
        }

        // GET: b_Global_Config/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            if (b_Global_Config == null)
            {
                return HttpNotFound();
            }
            return View(b_Global_Config);
        }

        // POST: b_Global_Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Global_Config b_Global_Config = db.b_Global_Config.Find(id);
            db.b_Global_Config.Remove(b_Global_Config);
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
