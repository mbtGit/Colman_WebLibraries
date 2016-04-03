using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName, Password")]User usrUser)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                var wantedUser = db.Users.Where(x => x.UserName.Equals(usrUser.UserName) && x.Password.Equals(usrUser.Password)).ToList();
                if (wantedUser.Count > 0)
                {
                    TempData["UserName"] = wantedUser[0].UserName;

                    if (wantedUser[0].IsAdmin)
                    {
                        TempData["Admin"] = true;
                        return RedirectToAction("AdminIndex", "Home");
                    }

                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
        }

        //
        // GET: /Login/
        public ActionResult Logout()
        {
            ViewBag.Admin = null;
            ViewBag.UserName = null;
            TempData["LoggedOut"] = true;
            //return Json("");
            //return Json(new 
            //{ 
            //    redirect = "true", 
            //    url = Url.Action("Index", "Home") 
            //});
            return RedirectToAction("Index", "Home");
        }

	}
}