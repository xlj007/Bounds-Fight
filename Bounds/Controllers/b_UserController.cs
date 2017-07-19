using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bounds.Models;
using System.Web.Security;
using Bounds.Utils;

namespace Bounds.Controllers
{
    
    public class b_UserController : Controller
    {
        private BoundsContext db = new BoundsContext();

        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(b_User b_user)
        {
            if (!ModelState.IsValid)
            {
                return View(b_user);
            }
            string strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(b_user.b_Password, "MD5");
            var result = from user in db.b_User
                         where user.b_UserName == b_user.b_UserName
                         where user.b_Password == strPassword
                         select user;
            if (result.Count() > 0)
            {
                Session["User"] = result.FirstOrDefault();
                Session["Enterprise_id"] = result.FirstOrDefault().b_Enterprise_ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("key", "用户名或密码错误，请重新登录！");
                return View(b_user);
            }
        }
        // GET: b_User
        [AuthorAdmin]
        public ActionResult Index()
        {
            ViewBag.Model = db.b_Organize;
            return View(db.b_User.ToList());
        }

        // GET: b_User/Details/5
        [AuthorAdmin]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_User b_User = db.b_User.Find(id);
            if (b_User == null)
            {
                return HttpNotFound();
            }
            return View(b_User);
        }

        // GET: b_User/Create
        [AuthorAdmin]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: b_User/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorAdmin]
        public string Create([Bind(Include = "ID,b_UserName,b_RealName,b_Sex,b_Password,b_WorkNum,b_Email,b_PhoneNum,b_Depart_ID,b_EntryDate,b_Role_ID,b_Reward_Auth_ID,b_Ranking,b_Create_Time,b_Update_Time")] b_User b_User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    b_User.b_Create_Time = DateTime.Now;
                    b_User.b_Update_Time = DateTime.Now;
                    b_User.b_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(b_User.b_Password, "MD5");
                    b_User.b_Enterprise_ID = Session["Enterprise_Id"].to_i();
                    db.b_User.Add(b_User);
                    db.SaveChanges();
                    return "OK";
                }
                return "ModelState不可用";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET: b_User/Edit/5
        [AuthorAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_User b_User = db.b_User.Find(id);
            if (b_User == null)
            {
                return HttpNotFound();
            }
            return View(b_User);
        }

        // POST: b_User/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorAdmin]
        public ActionResult Edit([Bind(Include = "ID,b_UserName,b_RealName,b_Sex,b_Password,b_WorkNum,b_Email,b_PhoneNum,b_Depart_ID,b_EntryDate,b_Role_ID,b_Reward_Auth_ID,b_Ranking,b_Create_Time,b_Update_Time")] b_User b_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_User);
        }

        // GET: b_User/Delete/5
        [AuthorAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_User b_User = db.b_User.Find(id);
            if (b_User == null)
            {
                return HttpNotFound();
            }
            return View(b_User);
        }

        // POST: b_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorAdmin]
        public ActionResult DeleteConfirmed(int id)
        {
            b_User b_User = db.b_User.Find(id);
            db.b_User.Remove(b_User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AuthorAdmin]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AuthorAdmin]
        [HttpPost]
        public ActionResult LogOff()
        {
            Session["User"] = null;
            return RedirectToAction("Logon");
        }
    }
}
