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
using System.Data.Entity.Infrastructure;

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
                string[] arrRole = null;
                if (result.FirstOrDefault().b_Role_ID != null)
                {
                    arrRole = result.FirstOrDefault().b_Role_ID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    IEnumerable<int> user_auth = from auth in db.b_User_Auth
                                                 where (arrRole.Contains(auth.b_Role_ID.ToString()))
                                                 select auth.b_Auth_ID;
                    Session["Author"] = user_auth.ToArray();
                }
                Session["User"] = result.FirstOrDefault();
                Session["UserName"] = b_user.b_UserName;
                Session["Enterprise_id"] = result.FirstOrDefault().b_Enterprise_ID;

                var Cus_Report = from report in db.b_Cus_Report
                                 where report.b_Enterprise_ID == b_user.b_Enterprise_ID
                                 select report;
                Session["CusReport"] = Cus_Report.ToArray();
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

        [AuthorAdmin]
        public ActionResult GetUserInfo(int user_id)
        {
            try
            {
                b_User user = db.b_User.Where(x=>x.ID == user_id).AsNoTracking().FirstOrDefault();
                string strDepart_Name = string.Empty;
                if (user.b_Depart_ID != null)
                {
                    string[] arrDepart = user.b_Depart_ID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strDepart_id in arrDepart)
                    {
                        b_Organize org = db.b_Organize.Find(strDepart_id.to_i());
                        strDepart_Name += org.b_Name + ",";
                    }
                }

                if (strDepart_Name != string.Empty)
                {
                    strDepart_Name = strDepart_Name.Substring(0, strDepart_Name.Length - 1);
                }
                user.b_Password = user.b_Depart_ID;
                user.b_Depart_ID = strDepart_Name;
                return Json(user);
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取用户信息时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [AuthorAdmin]
        public ActionResult DeleteUser(string user_ids)
        {
            try
            {
                int[] arrIds = Array.ConvertAll(user_ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));
                if (arrIds.Contains(((b_User)Session["User"]).ID))
                {
                    return Json("用户不能删除自己，请确认。");
                }
                else
                {
                    var users = from u in db.b_User
                                where arrIds.Contains(u.ID)
                                select u;
                    db.b_User.RemoveRange(users);
                    db.SaveChanges();
                    return Json("删除成功。");
                }
            }
            catch (Exception ex)
            {
                Log.logger.Error("删除用户信息时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
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
                b_User.b_Create_Time = DateTime.Now;
                b_User.b_Update_Time = DateTime.Now;
                b_User.b_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(b_User.b_Password, "MD5");
                b_User.b_Enterprise_ID = Session["Enterprise_id"].to_i();
                
                db.b_User.Add(b_User);
                db.SaveChanges();

                string[] arrRoles = null;
                if (b_User.b_Role_ID != null)
                {
                    arrRoles = b_User.b_Role_ID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (arrRoles != null)
                {
                    b_User_Role role = null;
                    foreach (string strRole in arrRoles)
                    {
                        role = new b_User_Role();
                        role.b_User_Id = b_User.ID;
                        role.b_Role_Id = strRole.to_i();
                        db.b_User_Role.Add(role);
                    }
                    db.SaveChanges();
                }
                return "OK";
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
        [AuthorAdmin]
        public ActionResult Edit([Bind(Include = "ID,b_UserName,b_RealName,b_Sex,b_Password,b_WorkNum,b_Email,b_PhoneNum,b_Depart_ID,b_EntryDate,b_Role_ID,b_Reward_Auth_ID,b_Ranking,b_Create_Time,b_Update_Time")] b_User b_User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    b_User org_user = db.b_User.Find(b_User.ID);
                    if (String.IsNullOrEmpty(b_User.b_Password))
                    {
                        b_User.b_Password = org_user.b_Password;
                    }
                    else
                    {
                        b_User.b_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(b_User.b_Password, "MD5");
                    }
                    b_User.b_Create_Time = org_user.b_Create_Time;
                    b_User.b_Enterprise_ID = Session["Enterprise_id"].to_i();
                    b_User.b_Update_Time = DateTime.Now;
                    string[] arrRoles = null;
                    if (org_user.b_Role_ID != null)
                    {
                        arrRoles = org_user.b_Role_ID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (arrRoles != null)
                    {
                        b_User_Role role = null;
                        foreach (string strRole in arrRoles)
                        {
                            int nRole_id = strRole.to_i();
                            role = db.b_User_Role.Where(roleid => roleid.b_Role_Id == nRole_id).FirstOrDefault();
                            if (role != null)
                            {
                                db.b_User_Role.Remove(role);
                            }
                        }
                    }

                    string[] arrNewRoles = null;
                    if (b_User.b_Role_ID != null)
                    {
                        arrNewRoles = b_User.b_Role_ID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (arrNewRoles != null)
                    {
                        b_User_Role role = null;
                        foreach (string strRole in arrNewRoles)
                        {
                            role = new b_User_Role();
                            role.b_User_Id = b_User.ID;
                            role.b_Role_Id = strRole.to_i();
                            db.b_User_Role.Add(role);
                        }
                    }

                    RemoveHoldingEntityInContext(org_user);
                    db.Entry(b_User).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json("OK");
            }
            catch (Exception ex)
            {
                Log.logger.Error("修改用户信息出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }

        private Boolean RemoveHoldingEntityInContext(b_User entity)
        {
            var objContext = ((IObjectContextAdapter)db).ObjectContext;
            var objSet = objContext.CreateObjectSet<b_User>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
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
                    int[] org_user = cur_user.Select(user=>user.b_User_Id).ToArray();
                    db.b_User_Role.RemoveRange(cur_user);

                    string[] arrUserIds = user_id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strUserId in arrUserIds)
                    {
                        b_User_Role b_user_role = new b_User_Role();
                        b_user_role.b_Role_Id = role_id;
                        b_user_role.b_User_Id = strUserId.to_i();
                        db.b_User_Role.Add(b_user_role);

                        if (org_user!=null && !org_user.Contains(strUserId.to_i()))
                        {
                            b_User user = db.b_User.Find(strUserId.to_i());
                            user.b_Role_ID += "," + role_id;
                            db.b_User.Add(user);
                            db.Entry(user).State = EntityState.Modified;
                        }
                    }
                    if (org_user != null)
                    {
                        foreach (int role in org_user)
                        {
                            if (!arrUserIds.Contains(role.ToString()))
                            {
                                b_User user = db.b_User.Find(role);
                                List<string> arrRoleID = user.b_Role_ID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                for (int i = 0; i < arrRoleID.Count; i++)
                                {
                                    if (arrRoleID[i] == role_id.ToString())
                                    {
                                        arrRoleID.RemoveAt(i);
                                    }
                                }
                                string strRoleID = String.Join(",", arrRoleID);
                                user.b_Role_ID = strRoleID;
                                db.b_User.Add(user);
                                db.Entry(user).State = EntityState.Modified;
                            }
                        }
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
