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
    public class b_RoleController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Role
        public ActionResult Index()
        {
            List<b_Role_Show> list = new List<b_Role_Show>();
            string ent_id = Session["Enterprise_id"].ToString();
            var role_list = from rl in db.b_Role
                            where rl.b_Enterprise == ent_id
                            select rl;
            foreach(var item in role_list.ToList())
            {
                b_Role_Show show = new b_Role_Show();
                show.ID = item.ID;
                show.b_Role_Name = item.b_Role_Name;
                show.b_Role_Description = item.b_Role_Description;
                show.b_Enterprise = item.b_Enterprise;
                show.b_Member_Count = (from ur in db.b_User_Role where ur.b_Role_Id == item.ID select ur).Count();
                list.Add(show);
            }
            return View(list);
        }

        // GET: b_Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Role b_Role = db.b_Role.Find(id);
            if (b_Role == null)
            {
                return HttpNotFound();
            }
            return View(b_Role);
        }

        // GET: b_Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Role/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,b_Role_Name,b_Role_Description,Created_Time,Updated_Time")] b_Role b_Role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var strInput = Request.Form[3];
        //        b_Role.Created_Time = DateTime.Now;
        //        b_Role.Updated_Time = DateTime.Now;
        //        db.b_Role.Add(b_Role);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(b_Role);
        //}
        public string Create([Bind(Include = "Name,Description,Auth_List")] b_Role_Save b_Role_Save)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    b_Role b_role = new b_Role();
                    b_role.b_Role_Name = b_Role_Save.Name;
                    b_role.b_Role_Description = b_Role_Save.Description;
                    b_role.b_Enterprise = Session["Enterprise_id"].ToString();
                    b_role.Created_Time = DateTime.Now;
                    b_role.Updated_Time = DateTime.Now;
                    db.b_Role.Add(b_role);
                    db.SaveChanges();

                    var org_auth = from a in db.b_User_Auth
                                   where a.b_Role_ID == b_role.ID
                                   select a;
                    if (org_auth.FirstOrDefault() != null)
                    {
                        db.b_User_Auth.RemoveRange(org_auth);
                    }

                    string[] arrAuth = b_Role_Save.Auth_List.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAuth in arrAuth)
                    {
                        b_User_Auth user_auth = new b_User_Auth();
                        user_auth.b_Role_ID = b_role.ID;
                        user_auth.b_Auth_ID = strAuth.to_i();
                        db.b_User_Auth.Add(user_auth);
                    }
                    db.SaveChanges();
                    ts.Complete();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                Log.logger.Error(ex.Message);
                return ex.Message;
            }
        }

        // GET: b_Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Role b_Role = db.b_Role.Find(id);
            if (b_Role == null)
            {
                return HttpNotFound();
            }
            return View(b_Role);
        }

        // POST: b_Role/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public string Edit([Bind(Include = "ID,Name,Description,Auth_List")] b_Role_Save b_Role_Save)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        b_Role b_role = (from role in db.b_Role
                                         where role.ID == b_Role_Save.ID
                                         select role).FirstOrDefault();
                        b_role.Updated_Time = DateTime.Now;
                        b_role.b_Role_Name = b_Role_Save.Name;
                        b_role.b_Role_Description = b_Role_Save.Description;
                        db.SaveChanges();

                        var org_auth = from a in db.b_User_Auth
                                       where a.b_Role_ID == b_role.ID
                                       select a;
                        if (org_auth.FirstOrDefault() != null)
                        {
                            db.b_User_Auth.RemoveRange(org_auth);
                        }

                        string[] arrAuth = b_Role_Save.Auth_List.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string strAuth in arrAuth)
                        {
                            b_User_Auth user_auth = new b_User_Auth();
                            user_auth.b_Role_ID = b_role.ID;
                            user_auth.b_Auth_ID = strAuth.to_i();
                            db.b_User_Auth.Add(user_auth);
                        }
                        db.SaveChanges();
                        ts.Complete();
                    }
                }
                return "OK";
            }
            catch(Exception ex)
            {
                Log.logger.Error("编辑角色权限时出现错误：" + Environment.NewLine + ex.Message);
                return ex.Message;
            }
        }

        // GET: b_Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Role b_Role = db.b_Role.Find(id);
            if (b_Role == null)
            {
                return HttpNotFound();
            }
            return View(b_Role);
        }

        // POST: b_Role/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                b_Role b_Role = db.b_Role.Find(id);
                db.b_Role.Remove(b_Role);

                var bua_query = db.b_User_Auth.Where(ua => ua.b_Role_ID == id);
                db.b_User_Auth.RemoveRange(bua_query);

                db.SaveChanges();
                ts.Complete();
            }
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
