﻿using System;
using System.Collections;//類別實作IEnumerable介面
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models.Carts
{
    /// <summary>
    /// 購物車類別
    /// </summary>
    [Serializable]  
    public class Cart : IEnumerable<CartItem> //類別實作IEnumerable介面
    {
        /// <summary>
        /// 儲存所有商品
        /// </summary>
        private List<CartItem> cartItems;
        /*
         * 將原本的cartItems從public改為private，
         * 這樣做的原因是我們應該要遵循物件導向的精神把購物車封裝起來，
         * 不讓外部的類別可以輕易地直接存取購物車中真正的商品列表。 
         */

        /// <summary>
        /// 建構子 Initializes a new instance of the <see cref="Cart"/> class.
        /// </summary>
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }


        /// <summary>
        /// 取得購物車內商品的總數量
        //// 新增一個屬性Count可以計算目前商品總數。
        /// </summary>
      public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
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


        //新增:將AddCartItem()方法改為AddProduct()，直接使用商品編號的方式將商品加入購物車中。
        /// <summary>
        /// 新增一筆Product，使用ProductId
        /// </summary>
        /// <param name="product">Product 物件</param>
        /// <returns></returns>
        private bool AddProduct(Product product)
        {
            //將 Product 轉換為 CartItem
            var cartItem = new Models.Carts.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1,
                //Day24 新增圖片 (新增項目，故前面的「Quantity = 1」後,要加個「,」。)
                DefaultImageURL = product.DefaultImageURL
            };

            //將 CartItem 加入到購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        public bool AddProduct(int productId)
        {
            var FindItem = this.cartItems.Where(w => w.Id == productId)
                .Select(s => s).FirstOrDefault();

            // 判斷相同 Id 的 CartItem 是否已經存在購物車內
            if (FindItem == default(Models.Carts.CartItem))
            {
                //若不存在於購物車內，則新增一筆
                using (Models.CartsEntities1 db = new CartsEntities1())
                {
                    var product = db.Products.Where(w => w.Id == productId)
                        .Select(s => s).FirstOrDefault();

                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {
                // 存在於購物車，則將商品數量累加
                FindItem.Quantity += 1;
            }
            return true;
        }

        //Day22新增RemoveProduct()方法，
        //如果ProductId有存在購物車則移除，若不存在則不做任何動作。
        /// <summary>
        /// 移除一筆 Product,使用productId
        /// </summary>
        ///  <param name="productId">The product identifier.</param>
        /// <returns></returns>

        public bool RemoveProduct(int productId)
        {
            var FindItem = this.cartItems.Where(w => w.Id == productId)
                .Select(s => s).FirstOrDefault();

            //判斷相同 Id 的 CartItem 是否已經存在購物車內
            if (FindItem != default(Models.Carts.CartItem))
             /*
                 {
                 //不存在購物車內，不需做任何動作
                  }
             else
            */
            {
                // 存在於購物車內，將商品移除
                this.cartItems.Remove(FindItem);
            }

            return true;
        }


        //Day23 新增ClearCart()方法，直接將購物車內的cartItems清空。
        /// <summary>
        /// 清空購物車
        /// </summary>
        /// <returns></returns>
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        //Day25
        public List<Models.OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<Models.OrderDetail>();
            foreach (var item in this.cartItems)
            {
                result.Add(new Models.OrderDetail()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }

        #region IEnumerator

        public IEnumerator<CartItem> GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cartItems).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cartItems).GetEnumerator();
        }
        #endregion
    }
}