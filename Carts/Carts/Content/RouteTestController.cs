using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Content
{
    public class RouteTestController : Controller
    {
        // GET: RouteTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2(string id)
        {
            return Content(
                string.Format("id的值為:{0}", id)
                );
        }

        public ActionResult Index3(string id, string page)
        {
            return Content(
                string.Format("id的值為:{0},page的值為:{1}",id,page)
                );
        }
    }
}