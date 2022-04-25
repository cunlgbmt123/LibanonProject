using LibanonProject.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace LibanonProject.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly BookContext db = new BookContext();
       /* private int Id = 1;*/
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

        public IEnumerable<Book> GetAll()
        {
            return db.books.ToList();
        }

        public Book GetById(int id)
        {
            Book books = db.books.Find(id);
            return books;
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
            books.CurrentUserId = item.CurrentUserId;
            books.BookISBN = item.BookISBN;
            db.SaveChanges();
            return true;
        }
    }
}