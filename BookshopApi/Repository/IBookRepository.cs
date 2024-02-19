using BookshopApi.DTO;
using BookshopApi.DTO.Book;
using BookshopApi.Models;
using System.Threading.Tasks;

namespace BookshopApi.Repository
{
    public interface IBookRepository : IBookshopRepository<Book>
    {
        Task<Book> GetBookDetailsAsync(int id);
        Task<Book> CreateBookA(int authorId, Book book);
        Task<Book> CreateBookAP(CreateBookDTO book);
        //Task<IEnumerable<BookDTO>> GetAllAsync2();

        /*        Task<IEnumerable<Book>> GetBooksAsync();
                Task<Book> GetBookDetailsAsync(int id);
                Task<Book> GetBookByIdAsync(int id, bool useNoTracking = false);
                Task<Book> GetBookByTitleAsync(string title);
                Task<int> CreateBookAsync(Book book);
                Task<Book> UpdateBookAsync(Book book);
                Task<bool> DeleteBookAsync(Book book);*/

    }
}
