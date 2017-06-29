using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models.Carts
{
    /// <summary>
    /// 購物車類別
    /// </summary>
    [Serializable]

    public class Cart
    {
        /// <summary>
        /// 儲存所有商品
        /// </summary>
        public List<CartItem> cartItems;

        /// <summary>
        /// 建構子 Initializes a new instance of the <see cref="Cart"/> class.
        /// </summary>
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        /// <summary>
        /// 取得商品總價
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach (var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }

                return totalAmount;
            }
        }
    }
}