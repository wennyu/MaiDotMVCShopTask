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
    }
}