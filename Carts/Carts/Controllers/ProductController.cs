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

            //接收轉導的成功訊息(將轉導的成功訊息設定給ViewBag變數)
            ViewBag.ResultMessage = TempData["ResultMessage"];
            
            //使用CartsEntities類別，名稱為db
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //使用LinQ語法抓取目前Products資料庫中所有資料
                result = (from s in db.Products select s).ToList();
            }
            //
               //將 result 傳送給檢視 
                return View(result);
        }

         //建立商品頁面
     public ActionResult Create()
     {
        return View();
     }

        //建立商品頁面—資料傳回處理
        [HttpPost] //指定只有使用POST方法才可進入
        public ActionResult Create(Models.Product postback)
        {

            /*判斷如果Product資料驗證通過，則設定成功訊息至TempData，並且轉導致Index頁面。 */

            //如果資料驗証成功
            if (this.ModelState.IsValid) 

            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //將回傳資料 postback加入至products
                db.Products.Add(postback);

                //儲存異動資料
                db.SaveChanges();

                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format($"商品[{postback.Name}]成功建立");

                    //轉導product/Index頁面
                    return RedirectToAction("Index");
            }

            //失敗訊息
            ViewBag.ResultMessage = "資料有誤，請檢查";

            //停留在Create頁面
            return View(postback);
        }

        //編輯商品頁面
        //新增Edit()方法，輸入為id，此Action是準備接收剛剛步驟1Index頁面新增的[Edit]超連結之請求(Request)。
        public ActionResult Edit(int id)
        {
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //抓取product.Id等於輸入id的資料
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();

                //判斷此id是否有資料
                if (result != default(Models.Product))
                {
                    //如果有，回傳編輯商品頁面
                    return View(result);
                }
                else
                {
                    //如果沒有資料，則顯示資料錯誤訊息，並導回Index頁面
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }


        //編輯商品頁面—資料傳回處理
        [HttpPost]
        /*由於原本Index頁面的Delete按鈕是使用Get操作，此將Delete()方法改為Post，所以伺服器找不到相對應的方法而產生錯誤。
         故須到Index頁面，
         將原本使用ActionLink的刪除按鈕改為使用BeginForm*/
        public ActionResult Edit(Models.Product postback)
        {
            //判斷使用者輸入資料是否正確
            if (this.ModelState.IsValid)
            {
                using (Models.CartsEntities1 db = new Models.CartsEntities1())
                {
                    //抓取Product.Id等於回傳postback.ID的資枓
                    var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();

                    //儲存使用者變更資料 
                    result.Name = postback.Name;
                    result.Price = postback.Price;
                    result.PublishDate = postback.PublishDate;
                    result.Quantity = postback.Quantity;
                    result.Status = postback.Status;
                    result.CategoryId = postback.CategoryId;
                    result.DefaultImageId = postback.DefaultImageId;
                    result.Description = postback.Description;
                    result.DefaultImageURL = postback.DefaultImageURL;//Edit() Action 內新增儲存DefaultImageURL的程式碼

                    //儲存所有變更
                    db.SaveChanges();

                    //設定成功訊息並導回index頁面
                    TempData["ResultMessage"] = String.Format($"商品[{postback.Name}]成功編輯");
                    return RedirectToAction("Index");
             
                }            
            }

            //如果資料不正確則導向自己(Edit頁面)
            else
            {
                return View(postback);
            }

        }


        //刪除商品（新增Deleete()方法，基本上與編輯商品Edit大同小異，唯一差別是Delete我們不需要產生View，因為無論刪除是成功還是失敗，一律導回Index頁面。）
        [HttpPost]
        //刪除是很重要的操作，如果使用者直接在網址列輸入網址就可以刪除的話，其實是很危險的一件事情，所以我們選擇使用Post來完成。
        public ActionResult Delete(int id)
        {
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //抓取Product.Id等於輸入id的資料
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();

                //判斷此id是否有資料
                if (result != default(Models.Product))
                {
                    db.Products.Remove(result);

                    //儲存所有變更
                    db.SaveChanges();

                    //設定成功訊息並導回index頁面
                    TempData["ResultMessage"] = String.Format($"商品[{result.Name}]成功刪除");
                    return RedirectToAction("Index");
                }

                else
                {//如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["resultMessage"]= "指定資料不存在，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }



    }

}