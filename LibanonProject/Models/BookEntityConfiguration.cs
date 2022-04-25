using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LibanonProject.Models
{
    internal class BookEntityConfiguration : EntityTypeConfiguration<Book>
    {
        public BookEntityConfiguration()
        {
            
            this.ToTable("Book");
            this.HasKey<int>(s => s.BookId);
            this.Property(p => p.BookId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(s => s.BookISBN)
                 .WithRequiredPrincipal(a => a.Book);

            this.HasRequired<User>(s => s.CurrentUser)
                .WithMany(g => g.Book)
                .HasForeignKey<int>(s => s.CurrentUserId);

            
                 

            
        }
       
    }
}