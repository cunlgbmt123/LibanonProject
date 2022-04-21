using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class BookContext : DbContext, IBookContext
    {
        public BookContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Book> books { get; set; }
        public DbSet<BookISBN> bookISBN { get; set; }
        public DbSet<User> User { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookEntityConfiguration());

            modelBuilder.Configurations.Add(new BookISBNEntityConfiguration());

            modelBuilder.Configurations.Add(new UserEntityConfiguration());

            
        }
      
    }
    
}