using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class Book
    {
        public Book()
        {
            Image = "~/Content/IMG/";

        }
        public int BookId { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string Image { get; set; }
        public DateTime Publisher { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public bool IsBorrow { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageURL { get; set; }


        public virtual BookISBN BookISBN { get; set; }

        public int CurrentUserId { get; set; }
        public virtual User CurrentUser { get; set; }

        public ICollection<BorrowBook> BorrowBooks { get; set; }


    }
}