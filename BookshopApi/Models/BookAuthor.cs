using System.ComponentModel.DataAnnotations;

namespace BookshopApi.Models
{
    public class BookAuthor
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        //public byte? AuthorOrder { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;
    }
}
