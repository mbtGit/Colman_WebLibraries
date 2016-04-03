using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }
        [DisplayName("סיסמה")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}