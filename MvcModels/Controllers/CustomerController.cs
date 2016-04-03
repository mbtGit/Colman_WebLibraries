using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class CustomersViewModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
    }
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                return View(db.Customers.ToList());
            }
        }

        // GET: /Customer/LoanedBooks
        public ActionResult LoanABook(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.WantedBookId = Id;
                return View(db.Customers.ToList());
            }
        }

        // GET: /Customer/LoanedBooks
        public ActionResult LoanTheBook(int CustomerId, int BookId)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                BookCustomerManyToMany bkcst = new BookCustomerManyToMany();
                bkcst.CustomerId = CustomerId;
                bkcst.BookId = BookId;

                db.BooksToCustomers.Add(bkcst);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        // GET: /Customer/LoanedBooks
        public ActionResult LoanedBooks()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                var manyToMany = from mtm in db.BooksToCustomers
                                 join bk in db.Books on mtm.BookId equals bk.Id
                                 join cst in db.Customers on mtm.CustomerId equals cst.Id
                                 select new CustomersViewModel { Id = mtm.CustomerId, BookName = bk.BookName };

                ViewBag.BookLoanes = manyToMany.ToList();

                return View(db.Customers.ToList());
            }
        }

        //
        // GET: /Customer/Create
        public ActionResult Create()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "הוספת לקוח";
                return View();
            }
        }

        //
        // Post: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id ,FullName, LoanedBooks, Address")]Customer cstNewCustomer)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                db.Customers.Add(cstNewCustomer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Edit the customer details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "עריכת לקוח";
                return View(db.Customers.Find(Id));
            }
        }

        //
        // Post: /Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FullName, LoanedBooks, Address")]Customer cstCustToEdit)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Customer cstCustomer = db.Customers.Find(cstCustToEdit.Id);
                cstCustomer.Address = cstCustToEdit.Address;
                cstCustomer.FullName = cstCustToEdit.FullName;
                cstCustomer.LoanedBooks = cstCustToEdit.LoanedBooks;
                
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Delete the customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? Id)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                ViewBag.Title = "מחיקת לקוח";
                Customer cstCustomerToDelete = db.Customers.Find(Id);
                return View(cstCustomerToDelete);
            }
        }

        /// <summary>
        /// Post: Delete the customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, FullName, LoanedBooks, Address")]Customer cstCustToDelete)
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                Customer cstCustomerToDelete = db.Customers.Find(cstCustToDelete.Id);
                db.Customers.Remove(cstCustomerToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Search the customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strCustomerName, string strCustomerId, string strCustomerAddress)
        {

            using (LibraryDbContext db = new LibraryDbContext())
            {
                if (strCustomerName == null && strCustomerId == null && strCustomerAddress == null)
                {
                    ViewBag.Title = "חיפוש לקוח";
                    return View(db.Customers.ToList());
                }
                else
                {
                    return View(db.Customers.Where(x => x.Id.ToString().Contains(strCustomerId) &&
                        x.FullName.Contains(strCustomerName) &&
                        x.Address.Contains(strCustomerAddress)).ToList());
                }
            }
        }
	}
}