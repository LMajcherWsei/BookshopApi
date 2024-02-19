using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookshopApi.DTO.Book
{
    public class CreateBookDTO
    {
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
/*        public string? AuthorFristName { get; set; }
        public string? AuthorMiddleName { get; set; }
        public string? AuthorLastName { get; set; }*/
        
        // Maybe to add more authors..!!
        //public List<Author?> Authors { get; set; }

        public string? PublisherName { get; set;}

        [Url]
        public string? PhotoUrl { get; set; }

        public string? Description { get; set; }

        public int? Pages { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public double? Rate { get; set; }

        public string? Series { get; set; }

        public string? PublicationDate { get; set; }

        public string? Language { get; set; }
    }
}
