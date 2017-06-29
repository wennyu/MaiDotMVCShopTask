using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models.Carts
{

    /// <summary>
    /// 購物車內單一商品類別
    /// </summary>
    [Serializable]
    public class CartItem
    {
        /// <summary>
        /// 商品編號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品購買時的價格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 商品購買數量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 商品小計
        /// </summary>
        public decimal Amount
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }

}