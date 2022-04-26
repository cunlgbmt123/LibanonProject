using LibanonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibanonProject.Repository
{
    public class BorrowRepo : IBorrowRepo
    {
        private readonly BookContext db = new BookContext();
        public BorrowRepo()
        {

        }
            
        public bool Borrow(BorrowBook item)
        {
            if (item == null)
            {
                throw new ArgumentException("item");
            }

           
            db.BorrowBooks.Add(item);

            db.SaveChanges();

            return true;
        }

        public IEnumerable<BorrowBook> GetAll()
        {
            return db.BorrowBooks.ToList();
        }

        public BorrowBook GetById(int id)
        {
            BorrowBook borrow = db.BorrowBooks.Find(id);
            return borrow;
        }

        public bool GiveBack(BorrowBook item)
        {
            if (item == null)
            {
                throw new ArgumentException("item");
            }


            db.BorrowBooks.Add(item);

            db.SaveChanges();

            return true;
        }
    }
}