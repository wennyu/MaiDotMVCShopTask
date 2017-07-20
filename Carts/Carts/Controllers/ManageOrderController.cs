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
                return View(result);
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
              /*Day28 
             ManageOrderController中新增SearchByUserName()，
             由於我們網站的使用者與訂單是兩個不同的資料庫，
             故我們先使用UserName搜尋出UserId後，
             再至Orders表查詢該UserId的所有訂單。
             將結果丟給Index()的View   */

        [ValidateAntiForgeryToken]
        public ActionResult SearchByUserName(string userName)
        {
            // 儲存查詢出來的 UserId
            string SearchUserId = null;
            using (Models.UserEntities db = new Models.UserEntities())
            {
                SearchUserId = db.AspNetUsers
                    .Where(w => w.UserName == userName)
                    .Select(s => s.Id).FirstOrDefault();
            }

            //如果有存在UserId
            if (!string.IsNullOrEmpty(SearchUserId))
                {
                //找出該UserId的所有訂單
                using (Models.CartsEntities1 db = new Models.CartsEntities1())
                {
                    var result = db.Orders
                        .Where(w => w.UserId == SearchUserId)
                        .Select(s => s).ToList();

                    //回傳結果至Index頁面
                    return View("Index", result);
                }
            }

            //回傳空結果至Index View
            return View("Index", new List<Models.Order>());
        }   
    }
}
