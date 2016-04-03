using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class LibraryController : Controller
    {
        //
        // GET: /Library/
        public ActionResult Index()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "מפת ספריות";
                return PartialView(db.Libraries.ToList());
            }
        }
        //
        // GET: /Library/
        public ActionResult Create()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "הוספת ספריה";
                return PartialView();
            }
        }

        //
        // Post: /Library/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind (Include = "LibraryAddress, LibraryName")]Library libNewLibrary)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                db.Libraries.Add(libNewLibrary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// Edit: Edit the library details
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Library lib = db.Libraries.Find(Id);
                return PartialView(lib);
            }
        }

        /// <summary>
        /// Post: Edit the library details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, LibraryAddress, LibraryName")]Library libNewLibrary)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Library libWanted = db.Libraries.Find(libNewLibrary.Id);
                libWanted.LibraryAddress = libNewLibrary.LibraryAddress;
                libWanted.LibraryName = libNewLibrary.LibraryName;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Delete the library
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Library libLibraryToDelete = db.Libraries.Find(Id);
                return PartialView(libLibraryToDelete);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, LibraryAddress, LibraryName")]Library libToDelete)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Library libLibraryToDelete = db.Libraries.Find(libToDelete.Id);
                db.Libraries.Remove(libLibraryToDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Show all the books in the libraries
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BooksInLibrary()
        {
            return PartialView();
        }

        /// <summary>
        /// Post: Edit all the books in specific library
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooksInLibrary(int Id)
        {
            return PartialView();
        }

        /// <summary>
        /// Post: Search the book
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strLibraryAddress)
        {

            using (LibraryDbContext db = new LibraryDbContext())
            {
                if (strLibraryAddress == null)
                {
                    ViewBag.Title = "חיפוש ספריה";
                    return View(db.Libraries.ToList());
                }
                else
                {
                    return View(db.Libraries.Where(x => x.LibraryAddress.Contains(strLibraryAddress)).ToList());
                }
            }
        }
	}
}