using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//新增ManageUserController，提供會員列表與會員編輯功能，包含Index()、Edit()等方法

namespace Carts.Controllers
{
    public class ManageUserController : Controller
    {

        // GET: ManageUser
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (Models.UserEntities db = new Models.UserEntities())
            {   
                //抓取所有AspNetUsers中的資料，並且放入Models.ManageUser模型中
                var result = (from s in db.AspNetUsers
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).
                              ToList();
                return View(result);
            }

            /*或寫作
              var result = db.AspNetUsers.Select(
                    s => new Models.ManageUser
                    {
                        Id = s.Id,
                        UserName = s.UserName,
                        Email = s.Email
                    });
                return View(result.ToList());                       
             */
        }

        public ActionResult Edit(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == id
                              select new Models.ManageUser
                              /*或寫為
                                var result = db.AspNetUsers.Where(w => w.Id == id)
                                  .Select(s => new Models.ManageUser
                                  */
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).FirstOrDefault();

                if (result != default(Models.ManageUser))
                {
                    return View(result);
                }
            }

            //設定錯誤訊息
            TempData["ResultMessage"] = $"使用者 {id} 不存在，請重新操作。";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();

                if (result != default(Models.AspNetUsers))
                {
                    result.UserName = postback.UserName;
                    result.Email = postback.Email;

                    db.SaveChanges();

                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.UserName);
                    //可寫作  TempData["ResultMessage"] = $"使用者 {model.UserName} 成功編輯。";
                    return RedirectToAction("Index");
                }
            }

            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.UserName);
            //可寫作    TempData["ResultMessage"] = $"使用者 {model.UserName} 不存在，請重新操作。";
            return RedirectToAction("Index");
        }


        public ActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                // 設定錯誤訊息
                TempData["ResultMessage"] = $"使用者 {id} 不存在，請重新操作。";
                return RedirectToAction("Index");
            }

            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = db.AspNetUsers.Find(id);
                return View(result);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = db.AspNetUsers.Find(id);
                if (result != default(Models.AspNetUsers))
                {
                    db.AspNetUsers.Remove(result);

                    // 儲存所有變更
                    db.SaveChanges();

                    // 設定成功訊息，並導回 Index 頁面
                    TempData["ResultMessage"] = $"使用者 {result.UserName} 已成功刪除";
                    return RedirectToAction("Index");
                }

                // 如果沒有資料，顯示錯誤訊息，並將頁面導回 Index 頁面。
                TempData["ResultMessage"] = $"使用者 {id} 不存在，無法刪除，請重新操作。";
                return RedirectToAction("Index");
            }
        }



    }
}