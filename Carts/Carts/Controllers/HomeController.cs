using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            //修改首頁為正常的購物網站首頁
            //將HemeController的Index()改為抓取Product表所有資料
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                var result = db.Products.ToList();
                return View(result);
            }
        }

        public ActionResult Index2()
        {
            return Content(
                "<html><body><h1>這是一段訊息</html></body></h1>"
                );
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Day29 新增Details()，主要為顯示某個編號之商品詳細資訊
        public ActionResult Details(int id)
        {
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                var result = db.Products
                    .Where(w => w.Id == id)
                    .Select( s => s).FirstOrDefault();

                if (result == default(Models.Product))
                {
                    return RedirectToAction("Index");
                }
                return View(result);
            }
        }

        [HttpPost]// 限定使用POST
        [Authorize] // 登入會員才能留言
        public ActionResult AddComment(int id, string content)
        {
            // 取得目前登入使用者 Id
            var UserId = HttpContext.User.Identity.GetUserId();

            var CurrentDateTime = DateTime.Now;

            var Comment = new Models.ProductComment()
            {
                ProductId = id,
                Content = content,
                UserId = UserId,
                CreateDate = CurrentDateTime
            };

            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                db.ProductComments.Add(Comment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = id });
        }

    }
}