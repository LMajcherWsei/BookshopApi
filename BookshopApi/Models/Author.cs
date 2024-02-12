using System.ComponentModel.DataAnnotations;

namespace BookshopApi.Models
{
    public class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Descripiton { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
