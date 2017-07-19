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

namespace Bounds.Controllers
{
    [AuthorAdmin]
    public class b_OrganizeController : Controller
    {
        private BoundsContext db = new BoundsContext();

        // GET: b_Organize
        public ActionResult Index()
        {            
            return View(db.b_Organize.ToList());
            //return null;
        }
        [HttpPost]
        public ActionResult SaveNew(int NodeId, string NodeValue)
        {
            try
            {
                var existNode = from orgEx in db.b_Organize
                                where orgEx.b_PID == NodeId
                                where orgEx.b_Name == NodeValue
                                select orgEx;
                if (existNode.Count() > 0)
                {
                    return Json("已经存在相同的元素，请更换名称后再保存！");
                }
                else
                {
                    b_Organize org = new b_Organize();
                    org.b_PID = NodeId;
                    org.b_Name = NodeValue;
                    org.b_Enterprise_Id = Convert.ToInt32(Session["Enterprise_id"]);
                    db.b_Organize.Add(org);
                    db.SaveChanges();
                    return Json("true");
                }
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存新部门出错：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult SaveEdit(int NodeId, string NodeValue)
        {
            try
            {
                var node = from nodes in db.b_Organize
                           where nodes.ID == NodeId
                           select nodes;
                var node_org = node.FirstOrDefault();
                var node_new = from nodes in db.b_Organize
                               where nodes.b_Name == NodeValue
                               where nodes.b_PID == node_org.b_PID
                               where nodes.ID != node_org.ID
                               select nodes;
                if (node_new.Count() > 0)
                {
                    return Json("已经存在同名节点，请更改名称后保存。");
                }
                else
                {
                    node_org.b_Name = NodeValue;
                    db.SaveChanges();
                    return Json("true");
                }
            }
            catch (Exception ex)
            {
                Log.logger.Error("修改部门名称是出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult DeleteOrg(int NodeId)
        {
            try
            {
                var node = db.b_Organize.Find(NodeId);
                if (node != null)
                {
                    db.b_Organize.Remove(node);
                    db.SaveChanges();
                    return Json("true");
                }
                else
                {
                    return Json("此节点已经被删除，请刷新页面进行确认。");
                }
            }
            catch (Exception ex)
            {
                Log.logger.Error("删除节点时出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult GetCheck(int NodeId)
        {
            try
            {
                var node = db.b_Organize.Find(NodeId);
                var b_First_User = db.b_User.Find(node.b_First_User_id);
                var b_Final_User = db.b_User.Find(node.b_Final_User_id);
                string strCheck = "{\"b_First_User\":\"" + ((b_First_User == null) ? string.Empty : b_First_User.b_RealName) + "\",\"b_Final_User\":\"" + ((b_Final_User == null) ? string.Empty : b_Final_User.b_RealName) + "\"}";
                return Json(strCheck);
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取审核人员出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult SaveCheck(int NodeId, string FirstCheck, string FinalCheck)
        {
            try
            {
                var node = db.b_Organize.Find(NodeId);
                int nEnterprise_id = Convert.ToInt32(Session["Enterprise_Id"]);
                var b_First_User = from user in db.b_User
                                   where user.b_Enterprise_ID == nEnterprise_id
                                   where user.b_RealName == FirstCheck
                                   select user;
                var b_Final_User = from user in db.b_User
                                   where user.b_Enterprise_ID == nEnterprise_id
                                   where user.b_RealName == FinalCheck
                                   select user;
                if (b_First_User != null)
                {
                    node.b_First_User_id = b_First_User.FirstOrDefault().ID;
                }
                if (b_Final_User != null)
                {
                    node.b_Final_User_id = b_Final_User.FirstOrDefault().ID;
                }
                db.SaveChanges();
                return Json("true");
            }
            catch (Exception ex)
            {
                Log.logger.Error("保存审核人员出现错误：" + ex.Message);
                return Json(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult ShowTree(int nPid)
        {
            try
            {
                return Json(GetOrganizeList(nPid), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.logger.Error("获取组织机构树出错：" + ex.Message);
                return null;
            }
        }
        private IEnumerable<b_Organize> GetOrganizeList(int nPid)
        {
            var list = from dep in db.b_Organize
                       where dep.b_PID == nPid
                       select dep;
            foreach (var item in list.ToList())
            {
                b_Organize org = new b_Organize();
                org.ID = item.ID;
                org.b_PID = item.b_PID;
                org.b_Name = item.b_Name;
                org.b_First_User_id = item.b_First_User_id;
                org.b_Final_User_id = item.b_Final_User_id;
                org.children = GetOrganizeList(item.ID);
                org.open = true;
                yield return org;
            }
        } 

        // GET: b_Organize/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Organize b_Organize = db.b_Organize.Find(id);
            if (b_Organize == null)
            {
                return HttpNotFound();
            }
            return View(b_Organize);
        }

        // GET: b_Organize/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: b_Organize/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,b_PID,b_Name,b_First_User_id,b_Final_User_id")] b_Organize b_Organize)
        {
            if (ModelState.IsValid)
            {
                db.b_Organize.Add(b_Organize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(b_Organize);
        }

        // GET: b_Organize/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Organize b_Organize = db.b_Organize.Find(id);
            if (b_Organize == null)
            {
                return HttpNotFound();
            }
            return View(b_Organize);
        }

        // POST: b_Organize/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,b_PID,b_Name,b_First_User_id,b_Final_User_id")] b_Organize b_Organize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(b_Organize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b_Organize);
        }

        // GET: b_Organize/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            b_Organize b_Organize = db.b_Organize.Find(id);
            if (b_Organize == null)
            {
                return HttpNotFound();
            }
            return View(b_Organize);
        }

        // POST: b_Organize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            b_Organize b_Organize = db.b_Organize.Find(id);
            db.b_Organize.Remove(b_Organize);
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
