using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("מוציא לאור")]
        public string Publisher { get; set; }
        [Required]
        [DisplayName("שם הסופר")]
        public int BookWriter { get; set; }
        [Required]
        [DisplayName("שם הספר")]
        public string BookName { get; set; }
        [Required]
        [DisplayName("ז'אנר")]
        public string Genre { get; set; }
        [Required]
        [DisplayName("שנת הוצאה לאור")]
        public int PublishYear { get; set; }
        [Required]
        [DisplayName("תאריך הוספה")]
        public DateTime AddedDate { get; set; }
        [DisplayName("דירוג")]
        public double Rate { get; set; }
    }
}