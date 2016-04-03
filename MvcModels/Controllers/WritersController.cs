using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class WritersViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
    public class WritersController : Controller
    {
        //
        // GET: /Writers/
        public ActionResult Index()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "רשימת סופרים";
                var viewModel = (from bk in db.Books
                        group bk by  bk.BookWriter into wrt
                        select new WritersViewModel
                        {
                            Id = wrt.Key,
                            Count = wrt.Count()
                        }).ToList();
                ViewBag.WriterBookNum = viewModel;
                return View(db.Writers.ToList());
            }
        }

        //
        // GET: /Writers/Create
        public ActionResult Create()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "הוספת סופר";
                return View();
            }
        }

        //
        // Post: /Writers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName")]Writer wrtWriterToAdd)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                db.Writers.Add(wrtWriterToAdd);

                db.SaveChanges();

                return RedirectToAction("Index");;
            }
        }

        /// <summary>
        /// Post: Edit the writer details
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Writer wrtToEdit = db.Writers.Find(Id);

                return View(wrtToEdit);
            }
        }

        //
        // Post: /Writers/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FullName")]Writer wrtWriterToEdit)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Writer wrtObject = db.Writers.Find(wrtWriterToEdit.Id);
                wrtObject.FullName = wrtWriterToEdit.FullName;

                db.SaveChanges();

                return RedirectToAction("Index"); ;
            }
        }

        /// <summary>
        /// Post: Delete the writer
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Writer wrtToDelete = db.Writers.Find(Id);

                return View(wrtToDelete);
            }
        }


        //
        // Post: /Writers/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, FullName")]Writer wrtToDelete)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Writer wrtWriterToDelete = db.Writers.Find(wrtToDelete.Id);
                db.Writers.Remove(wrtWriterToDelete);

                db.SaveChanges();

                return RedirectToAction("Index"); ;
            }
        }

        /// <summary>
        /// Post: Search the writer
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strWriterName)
        {

            using (LibraryDbContext db = new LibraryDbContext())
            {
                if (strWriterName == null)
                {
                    ViewBag.Title = "חיפוש סופר";
                    return View(db.Writers.ToList());
                }
                else
                {
                    return View(db.Writers.Where(x => x.FullName.Contains(strWriterName)).ToList());
                }
            }
        }
	}
}