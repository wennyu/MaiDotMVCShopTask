using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


/*Day24.2 新增OrderController，包含Index()方法，
   此方法主要是顯示目前使用者的購物車內容，
   等待輸入好運送資訊後，即可以進入訂單資料庫*/

namespace Carts.Controllers
{
    [Authorize] //需登入才可以結帳
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Day25
        //OrderController 的Index() 定義如何將資料存入訂單資料庫(Order與OrderDetail資料表)，
        //其中順序為先寫入Order後，
        //再寫入OrderDetail。
        public ActionResult Index(Models.OrderModel.Ship model)
        {

            if (this.ModelState.IsValid)
            {
                //取得目前購物車
                var CurrentCart = Models.Carts.Operation.GetCurrentCart();

                //取得目前登入使用者 Id
                var UserId = HttpContext.User.Identity.GetUserId();

                using (Models.CartsEntities1 db = new Models.CartsEntities1())
                {
                    //建立Order物件
                    var order = new Models.Order()
                    {
                        UserId = UserId,
                        RecieverName = model.ReceiverName,
                        RecieverPhone = model.ReceiverPhone,
                        RecieverAddress = model.ReceiverAddress
                    };


                    // 加入 Order 資料表後，儲存變更
                    db.Orders.Add(order);
                    db.SaveChanges();


                    //取得購物車中的 OrderDeatil 物件
                    var orderDetails = CurrentCart.ToOrderDetailList(order.Id);

                    //將 OrderDetails 物件，加入OrderDetail資枓表後，儲存變更。
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return Content("訂購成功");
            }

            return View();
        }
    }
}