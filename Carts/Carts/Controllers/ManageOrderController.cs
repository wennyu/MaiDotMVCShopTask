using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Day26
//Index()是顯示目前網站所有訂單
//Details()是針對每筆訂單顯示購買商品清單

namespace Carts.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //取得Order中所有資料
                var result = db.Orders.Select(s => s).ToList();
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //取得 OrderId 為傳入 id 的所有商品列表
                var result = db.OrderDetails
                    .Where(w => w.OrderId == id)
                    .Select(s => s).ToList();

                if (result.Any())
                {
                    return View(result);
                }
                else
                {
                    //如果商品數目為零，代表該訂單異常 (無商品)。導回商品列表。
                    return RedirectToAction("Index");
                }
            }
        }
    }
}