using System.ComponentModel.DataAnnotations;

namespace BookshopApi.DTO.Book
{
    public class PrevierwBookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? PhotoUrl { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
