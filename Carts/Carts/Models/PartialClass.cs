using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*Day27
 在Models中新增PartialClass，擴充Order類別;
 其中定義Order的部分類別，定義GetUserName()方法，
 此方法主要是透過原本儲存在Order類別中的UserId去AspNetUsers表取得UrseName(使用者暱稱)
 */

namespace Carts.Models
{
    public class PartialClass
    {
    }
    /// <summary>
    /// 定義 Models.Order 的部分類別
    /// </summary>
    public partial class Order
    {
        /// <summary>
        /// 取得訂單中的使用者暱稱 Gets the name of the user.
        /// </summary>
        /// <returns>回傳 使用者暱稱</returns>
        public string GetUserName()
        {
            using (Models.UserEntities db = new UserEntities())
            {
                var result = db.AspNetUsers
                    .Where(w => w.Id == this.UserId)
                    .Select(s => s.UserName).FirstOrDefault();
                  
                //回傳找到的UserName
                return result;
            }
        }
    }

    /// <summary>
    /// 定義 Models.ProductComments 的部分類別
    /// </summary>
    public partial class ProductComment
    {
        /// <summary>
        /// 取得訂單中的使用者暱稱 Gets the name of the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>回傳 使用者暱稱</returns>
        public string GetUserName(string userId)
        {
            using (Models.UserEntities db = new UserEntities())
            {
                var result = db.AspNetUsers
                    .Where(w => w.Id == UserId)
                    .Select(s => s.UserName).FirstOrDefault();

                //回傳找到的UserName
                return result;
            }
        }
    }
}