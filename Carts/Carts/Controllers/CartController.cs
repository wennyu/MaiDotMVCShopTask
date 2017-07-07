using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
///新增取得當前購物車內容的GetCart()方法 與 新增一筆商品進購物車的AddToCart()方法，內部都是呼叫Cart的操作來完成。
/// </summary>

namespace Carts.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// 取得目前購物車頁面
        /// </summary>
        /// <returns>回傳購物車頁面</returns>
        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        /// <summary>
        /// 以id將 Product 加入至購物車
        /// </summary>
        /// <returns>回傳購物車頁面</returns>
        public ActionResult AddToCart(int id)
        {
            var CurrentCart = Models.Carts.Operation.GetCurrentCart();
            CurrentCart.AddProduct(id);
            return PartialView("_CartPartial");
        }


        //Day22 新增RemoveFromCart() Action ， 呼叫Cart類別的RemoveProduct()方法
        /// <summary>
        /// 從購物車移除 Product
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>回傳購物車頁面</returns>
        public ActionResult RemoveFromCart(int id)
        {
            var CurrentCart = Models.Carts.Operation.GetCurrentCart();
            CurrentCart.RemoveProduct(id);
            return PartialView("_CartPartial");
        }


        //Day23  新增ClearCart() Action ， 呼叫Cart類別的ClearCart()方法                
        /// <summary>
        /// Clears the cart. 清空購物車
        /// </summary>
        /// <returns>回傳購物車頁面</returns>
        public ActionResult ClearCart()
        {
            var CurrentCart = Models.Carts.Operation.GetCurrentCart();
            CurrentCart.ClearCart();
            return PartialView("_CartPartial");
        }

    }
}