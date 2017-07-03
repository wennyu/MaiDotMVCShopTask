using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class TestController : Controller
    {
        /*刪除預設
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        */

        /*增一個Action名稱為GetCart()，
         主要的功能為如果目前購物車沒有任何商品，則新增一筆假的商品．
         如果購物車內已經有商品，
         則將商品的數量加一．最後輸出目前購物車所有商品的總價．
         */
        public ActionResult GetCart()
        {
            // 取得目前購物車
            var Cart = Models.Carts.Operation.GetCurrentCart();
            Cart.AddProduct(1);

            /*D20 刪除（cartItems—建置錯誤）
            if (Cart.cartItems.Any())
            {
                Cart.cartItems.First().Quantity += 1;
            }
            else
            {
                Cart.cartItems.Add(
                    new Models.Carts.CartItem()
                    {
                        Id = 1,
                        Name = "測試",
                        Quantity = 1,
                        Price = 100m
                    });
            }*/

            return Content($"目前購物車總共：{Cart.TotalAmount} 元");
        }
    }
}
