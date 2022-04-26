using LibanonProject.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibanonProject.Repository
{
    public interface IBookRepo
    {
        
        
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetBookIsBorrow();
        IEnumerable<Book> GetBookOnShelf();
        Book GetById(int id);
        
        bool  Add(Book item);
        bool Update(Book item);
        void SendEmail(string MailTitle, string ToEmail, string MailContent);
        void UpdateBookStatus(Book item, bool status);
        void BorrowBook(int id, Book item);
        void DeleteBorrower(int id);
        void StateOfBook(Book item, bool? confirmLend, bool? confirmBorrow);
        void Rating(Book item);
    }
}
