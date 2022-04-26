using LibanonProject.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
namespace LibanonProject.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly BookContext db = new BookContext();
      
        public BookRepo()
        {

        }
        public bool  Add(Book item)
        {
            if(item == null)
            {
                throw new ArgumentException("item");
            }
            
            
            db.books.Add(item);

            db.SaveChanges();
           
            return true;
        }


        public void DeleteBorrower(int id)
        {
            Book book = db.books.Find(id);
            book.BorrowerName = null;
            book.BorrowerEmail = null;
            

            db.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            return db.books.ToList();
        }

        public IEnumerable<Book> GetBookIsBorrow()
        {
            return db.books.Where(x=>x.BookStatus == false).OrderBy(x=>x.BookId);
        }

        public IEnumerable<Book> GetBookOnShelf()
        {
            return db.books.Where(x => x.BookStatus == true).OrderBy(x => x.BookId);
        }

        public Book GetById(int id)
        {
            Book books = db.books.Find(id);
            return books;
        }

        public void SendEmail(string Title, string ToEmail, string Content)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail);
            mail.From = new MailAddress(ToEmail);
            mail.Subject = Title;
            mail.Body = Content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("mhoangd2000@gmail.com", "minhhoangdm185050247");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public bool Update(Book item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("item");
            }
            var books = db.books.Find(item.BookId);
            books.Title = item.Title;
            books.Author = item.Author;
            books.Image = item.Image;
            books.Publisher = item.Publisher;
            books.Category = item.Category;
            books.Summary = item.Summary;
            books.BorrowerName = item.BorrowerName;
            books.BorrowerEmail = item.BorrowerEmail;
            books.BorrowerPhone = item.BorrowerPhone;            
            books.User.OTP = item.User.OTP;
            books.BookStatus = true;
            db.SaveChanges();
            return true;
        }

        public void Rating(Book item)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookStatus(Book item, bool status)
        {
            Book bookStatus = db.books.Find(item.BookId);
            bookStatus.BookStatus = status;
            db.SaveChanges();
        }

        public void BorrowBook(int id, Book item)
        {
            Book book = db.books.Find(id);
            book.BorrowerName = item.BorrowerName;
            book.BorrowerEmail = item.BorrowerEmail;
            db.SaveChanges();
        }

        public void StateOfBook(Book item, bool? stateIsBorrow, bool? stateBorrow)
        {
            Book book = db.books.Find(item.BookId);
            if (stateIsBorrow != null)
                book.State.StateIsBorrow = (bool)stateIsBorrow;
            if (stateBorrow != null)
                book.State.StateBorrow = (bool)stateBorrow;
            if (book.State.StateIsBorrow == true && book.State.StateIsBorrow == true)
                UpdateBookStatus(book, true);
            if (book.State.StateIsBorrow == false && book.State.StateIsBorrow == false)
                UpdateBookStatus(book, false);
            db.SaveChanges();
        }
    }
}