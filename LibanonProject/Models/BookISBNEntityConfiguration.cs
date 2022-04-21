using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.ModelConfiguration;

namespace LibanonProject.Models
{
    internal class BookISBNEntityConfiguration : EntityTypeConfiguration<BookISBN>
    {
        public BookISBNEntityConfiguration()
        {
            this.HasKey(s => s.ISBNId);
            this.Property(p => p.ISBNId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
        }
    }
    public class Validator : AbstractValidator<BookISBN>
    {
        public Validator()
        {
            RuleFor(s => s.ISBNcode).NotEmpty().Length(10).WithMessage("Please specify ISBN code");
            RuleFor(s => s.Rating).NotEmpty().WithMessage("Please specify a first name");
        }
        
            
    }
}