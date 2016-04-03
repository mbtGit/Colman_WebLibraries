using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (TempData["UserName"] != null)
            {
                ViewBag.UserName = TempData["UserName"];
            }

            if (TempData["LoggedOut"] != null)
            {
                ViewBag.LoggedOut = true;
            }


            return View();
        }
        
        public ActionResult AdminIndex()
        {
            if (TempData["UserName"] != null)
            {
                ViewBag.UserName = TempData["UserName"];
            }

            ViewBag.Admin = true;
            return View();
        }
        
	}
}