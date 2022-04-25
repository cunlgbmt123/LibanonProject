using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class BorrowBook
    {
        public int ID { get; set; }
        public bool? Status { get; set; }
        public string BUser { get; set; }
        public string BEmail { get; set; }
        public string BPhone { get; set; }
        public Guid ActiveCode { get; set; }
        public string OTP { get; set; }

        public int CurrentBookId  { get; set; }
        public virtual Book Currentbook { get; set; }

     


    }
}