using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcModels.Models
{
    public class LibraryDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<BookCustomerManyToMany> BooksToCustomers { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public static class HtmlExtensions
    {
        public static string JsonSerialize(this HtmlHelper htmlHelper, object value)
        {
            return new JavaScriptSerializer().Serialize(value);
        }
    }
}