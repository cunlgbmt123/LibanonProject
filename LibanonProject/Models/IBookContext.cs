using System.Data.Entity;

namespace LibanonProject.Models
{
    public interface IBookContext
    {
        DbSet<BookISBN> bookISBN { get; set; }
        DbSet<Book> books { get; set; }
        DbSet<User> User { get; set; }
        int SaveChanges();
    }
}