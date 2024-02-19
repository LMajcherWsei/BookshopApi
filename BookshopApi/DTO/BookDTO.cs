using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BookshopApi.Models;

namespace BookshopApi.DTO
{
    public class BookDTO
    {
/*        public BookDTO()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }*/
        // TODO: Add more validation and more prop + maybe make validation attribute

        [Required]
        [DisplayName("Book ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        /*        public string? Author { get; set; }*/

        /* [Required]
         public List<Author?> Authors { get; set; }*/

        //public string? AuthorUrl { get; set; }
        [Url]
        public string? PhotoUrl { get; set; }

        public string? Description { get; set; }

        public int? Pages { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public double? Rate { get; set; }

        public string? Series { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string? Language { get; set; }

        public int PublisherId { get; set; }
        /*        
                public int PublisherId { get; set; }
                public string? PublisherName { get; set; } 
        */

        /*
                public virtual Publisher Publisher { get; set; }
                public virtual ICollection<BookAuthor> BookAuthors { get; set; }*/
    }
}
