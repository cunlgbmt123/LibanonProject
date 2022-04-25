using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LibanonProject.Models
{
    internal class BorrowBookEntityConfiguration : EntityTypeConfiguration<BorrowBook>
    {
        public BorrowBookEntityConfiguration()
        {
            this.HasKey<int>(p => p.ID);
            this.Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired<Book>(s => s.Currentbook)
                .WithMany(g => g.BorrowBooks)
                .HasForeignKey<int>(s => s.CurrentBookId);


            
               
        }
    }
}