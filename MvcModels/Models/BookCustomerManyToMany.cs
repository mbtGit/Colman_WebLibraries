using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class BookCustomerManyToMany
    {
        [Key]
        public int RelationId { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }
}