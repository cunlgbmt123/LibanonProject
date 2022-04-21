namespace LibanonProject.Models
{
    public class BookISBN
    {
        public int ISBNId { get; set; }
        public string ISBNcode { get; set; }
        public double Rating { get; set; }
        public virtual Book Book { get; set; }
    }
}