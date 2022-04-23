using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class BorrowBook
    {
        public int ID { get; set; }
        public string Status { get; set; }

        public int CurrentBookId  { get; set; }
        public virtual Book Currentbook { get; set; }

        public int CurrentUserId { get; set; }
        public virtual User CurrentUser { get; set; } 


    }
}