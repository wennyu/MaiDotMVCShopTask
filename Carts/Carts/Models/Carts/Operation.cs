using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Carts.Models.Carts
{
    /// <summary>
    /// 購物車操作類別
    /// </summary>
    public static class Operation
    {
        /// <summary>
        /// 取得目前 Session 中的 Cart 物件
        /// </summary>
        /// <returns> Session["Cart"] </returns>
        [WebMethod(EnableSession = true)]//取用 Session
        public static Models.Carts.Cart GetCurrentCart()
        {
            // 確認 System.Web.HttpContext.Current 是否為 NULL
            if (HttpContext.Current != null)
            {
                // 如果 Session["Cart"] 不存在，則新增一個空的 Cart 物件。
                if (HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Cart();
                    HttpContext.Current.Session["Cart"] = order;
                }
                // 回傳 Session["Cart"]
                return (Cart)System.Web.HttpContext.Current.Session["Cart"];
            }
            throw new InvalidOperationException("System.Web.HttpContext.Current 為 NULL，請檢查。");
        }

    }
}