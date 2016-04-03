using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("כתובת הספריה")]
        public string LibraryAddress { get; set; }
        [Required]
        [DisplayName("שם הספריה")]
        public string LibraryName { get; set; }
        
        public virtual List<Book> BooksInLibrary { get; set; }
    }
}