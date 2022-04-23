using System.Collections.Generic;

namespace LibanonProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }



        public ICollection<Book> Book { get; set; }

        public ICollection<BorrowBook> BorrowBooks { get; set; }
    }
}