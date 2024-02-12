using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookshopApi.Models
{
    public class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }
        // TODO: Add more validation and more prop + maybe make validation attribute

        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
/*        public string? Author { get; set; }*/

       /* [Required]
        public List<Author?> Authors { get; set; }*/

        //public string? AuthorUrl { get; set; }
        
        public string? PhotoUrl { get; set; }

        public string? Description { get; set; }

        public int? Pages { get; set; }

        public int PublisherId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public Double? Rate { get; set; }

        public string? Series { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string? Language { get; set; }


        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
