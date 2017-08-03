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
using System.Transactions;

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
                         where user.b_UserName == b_user.b_UserName && user.b_Password == strPassword && user.b_Enterprise_ID == b_user.b_Enterprise_ID
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

        private void SetViewBag(int ent_id)
        {
            string strEnterprise = ent_id.ToString();
            ViewBag.Model = db.b_Organize.Where(x => x.b_Enterprise_Id == ent_id).ToList();
            ViewBag.Role = db.b_Role.Where(x => x.b_Enterprise == strEnterprise).ToList();
            ViewBag.Reward_Auth = db.b_Reward.Where(x => x.b_Enterprise_ID == ent_id).ToList();
        }
        // GET: b_User
        [AuthorAdmin]
        public ActionResult Index()
        {
            int ent_id = Session["Enterprise_id"].to_i();
            SetViewBag(ent_id);
            return View(db.b_User.Where(x=>x.b_Enterprise_ID == ent_id).ToList());
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
            int ent_id = Session["Enterprise_id"].to_i();
            SetViewBag(ent_id);
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
                    b_User.b_Enterprise_ID = Session["Enterprise_id"].to_i();
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

        [AuthorAdmin]
        [HttpPost]
        public ActionResult GetRoleUser(int role_id)
        {
            int ent_id = Session["Enterprise_id"].to_i();
            var user = from u in db.b_User
                       join ur in db.b_User_Role on u.ID equals ur.b_User_Id
                       where ur.b_Role_Id == role_id && u.b_Enterprise_ID == ent_id
                       select new { u.ID, u.b_UserName };

            return Json(user.ToList());
        }

        [AuthorAdmin]
        [HttpPost]
        public ActionResult GetDepartUser(int depart_id)
        {
            List<int> list = new List<int>();
            list.Add(depart_id);
            list = GetDepartChild(depart_id, ref list);

            //get user
            List<IEnumerable<b_User>> list_user = new List<IEnumerable<b_User>>();
            int ent_id = Session["Enterprise_id"].to_i();
            foreach(int i in list)
            {
                string str = i.ToString();
                IEnumerable<b_User> users = from user in db.b_User
                                            where user.b_Enterprise_ID == ent_id && user.b_Depart_ID.Contains(str)
                                            select user;
                list_user.Add(users);
            }

            IEnumerable<b_User> cur_user = null;
            if (list_user.Count > 0)
            {
                cur_user = list_user[0];
                for (int i = 1; i < list_user.Count; i++)
                {
                    cur_user = cur_user.Union(list_user[i]);
                }
            }

            return Json(cur_user.Distinct().ToList());
        }

        private List<int> GetDepartChild(int depart_id, ref List<int> list)
        {
            var depart = from org in db.b_Organize
                         where org.b_PID == depart_id
                         select new { org.ID };
            foreach (var departid in depart.ToList())
            {
                list.Add(departid.ID);
                GetDepartChild(departid.ID, ref list);
            }
            return list;
        }

        [AuthorAdmin]
        [HttpPost]
        public ActionResult SaveRoleUser(int role_id, string user_id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var cur_user = from role in db.b_User_Role
                                   where role.b_Role_Id == role_id
                                   select role;
                    db.b_User_Role.RemoveRange(cur_user);

                    string[] arrUserIds = user_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strUserId in arrUserIds)
                    {
                        b_User_Role b_user_role = new b_User_Role();
                        b_user_role.b_Role_Id = role_id;
                        b_user_role.b_User_Id = strUserId.to_i();
                        db.b_User_Role.Add(b_user_role);
                    }
                    db.SaveChanges();
                    ts.Complete();
                }
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存角色绑定成员时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
    }
}
