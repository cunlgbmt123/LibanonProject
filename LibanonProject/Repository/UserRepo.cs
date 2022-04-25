using LibanonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibanonProject.Repository
{
    public class UserRepo : IUserRepo
    {
        readonly BookContext db = new BookContext();
        /*private int Id;*/
        public UserRepo()
        {

        }
        public User Add(User item)
        {
            if(item == null)
            {
                throw new ArgumentException("item");
            }           
            db.User.Add(item);
            db.SaveChanges();
            return item;
        }

        public IEnumerable<User> GetAll()
        {
            return db.User.ToList();
        }

        public User GetById(int id)
        {
            User user = db.User.Find(id);
            return user;
        }
    }
}