using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            //宣告回傳產品列表result
            List<Models.Product> result = new List<Models.Product>();

            //使用CartsEntities類別，名稱為db
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //使用LinQ語法抓取目前Products資料庫中所有資料
                result = (from s in db.Products select s).ToList();
            }

               //將 result 傳送給檢視 
                return View(result);
        }
    }
}