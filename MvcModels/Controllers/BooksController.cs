using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class BooksViewModel
    {
        public int BookId { get; set; }
        public string Writer { get; set; }
    }
    public class BooksController : Controller
    {
        //
        // GET: /Books/
        public ActionResult Index(string strCategory)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                var viewModel =
                    from wrt in db.Writers
                    join bk in db.Books on wrt.Id equals bk.BookWriter
                    select new BooksViewModel { BookId = bk.Id, Writer = wrt.FullName };
                ViewBag.Writers = viewModel.ToList();

                if (strCategory != null)
                {
                    ViewBag.Category = "ספרי " + strCategory;
                    return View(db.Books.Where(x => String.Equals(x.Genre, strCategory)).ToList());
                }
                else 
                {
                    ViewBag.Category = "כל הספרים";

                    return View(db.Books.ToList());
                }
            }
        }

        //
        // GET: /Books/Create
        public ActionResult Create()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "הוספת ספר";
                ViewBag.Writers = db.Writers.ToList();
                return View();
            }
        }

        //
        // Post: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Publisher, BookName, BookWriter, Genre, PublishYear")]Book bkNewBook)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                bkNewBook.AddedDate = DateTime.Now;
                db.Books.Add(bkNewBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Get: Edit the book details
        /// </summary>
        public ActionResult Edit(int? id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Book bkToEdit = db.Books.Find(id);
                return View(bkToEdit);
            }
        }

        /// <summary>
        /// Post: Edit the book details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Publisher, BookName, BookWriter, Genre, PublishYear")]Book bkToEdit)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Book bkWantToEdit = db.Books.Find(bkToEdit.Id);
                bkWantToEdit.Publisher = bkToEdit.Publisher;
                bkWantToEdit.BookName = bkToEdit.BookName;
                bkWantToEdit.BookWriter = bkToEdit.BookWriter;
                bkWantToEdit.Genre = bkToEdit.Genre;
                bkWantToEdit.PublishYear = bkToEdit.PublishYear;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Delete the book
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "מחיקת ספר";
                Book bkToDelete = db.Books.Find(id);
                return View(bkToDelete);
            }
        }

        /// <summary>
        /// Post: Delete the book
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, Publisher, BookName, BookWriter, Genre, PublishYear")]Book bkToDelete)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Book bkWantToDelete = db.Books.Find(bkToDelete.Id);
                db.Books.Remove(bkWantToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Search the book
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strBookName, string strBookGenre, string strBookPublishYear)
        {

            using (LibraryDbContext db = new LibraryDbContext())
            {
                if (strBookName == null && strBookGenre == null && strBookPublishYear == null) 
                { 
                    ViewBag.Title = "חיפוש ספר";
                    return View(db.Books.ToList());
                }
                else
                {
                    return View(db.Books.Where(x => x.BookName.Contains(strBookName) &&
                        x.PublishYear.ToString().Contains(strBookPublishYear) &&  
                        x.Genre.Contains(strBookGenre)).ToList());
                }
            }
        }
	}
}