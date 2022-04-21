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
                 .WithRequiredPrincipal(ad => ad.Book)
                 .WillCascadeOnDelete(true);
            
            this.HasRequired(s => s.User)
                .WithRequiredPrincipal(s => s.Book)
                .WillCascadeOnDelete(true);
        }
       
    }
}