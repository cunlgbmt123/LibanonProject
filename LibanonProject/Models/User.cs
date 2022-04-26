using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public Guid ActiveCode { get; set; }
        public string OTP { get; set; }

        public virtual Book Book { get; set; }
    }
}