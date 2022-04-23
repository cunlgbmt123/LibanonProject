
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity.ModelConfiguration;

namespace LibanonProject.Models
{
    internal class BookISBNEntityConfiguration : EntityTypeConfiguration<BookISBN>
    {
        public BookISBNEntityConfiguration()
        {
            this.HasKey(s => s.ISBNId);
            this.Property(p => p.ISBNId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.ISBNcode).HasMaxLength(10).IsRequired();
        }
    }
    
}