using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("תעודת זהות")]
        public int Id { get; set; }
        [Required]
        [DisplayName("שם מלא")]
        public string FullName { get; set; }
        public virtual List<int> LoanedBooks { get; set; }
        [DisplayName("כתובת מגורים")]
        public string Address { get; set; }
    }
}