using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*Day24.2 新增OrderController，包含Index()方法，
   此方法主要是顯示目前使用者的購物車內容，
   等待輸入好運送資訊後，即可以進入訂單資料庫*/

namespace Carts.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship model)
        {
            return View();
        }
    }
}