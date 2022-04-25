using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class BookISBN
    {
        [Key]
        public int ISBNId { get; set; }
        public string ISBNcode { get; set; }
        public double Rating { get; set; }


        public virtual Book Book { get; set; }
    }
}