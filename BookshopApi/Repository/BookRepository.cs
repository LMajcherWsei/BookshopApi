using AutoMapper;
using BookshopApi.Controllers;
using BookshopApi.Data;
using BookshopApi.DTO;
using BookshopApi.DTO.Book;
using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopApi.Repository
{
    public class BookRepository : BookshopRepository<Book>, IBookRepository
    {
        private readonly BooksContext _dbContext;
        private readonly IMapper _mapper;

        public BookRepository(BooksContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<BookPreviewDTO>> GetAllPreviewsAsync()
        {
            var books = await _dbContext.Books.Include(a => a.BookAuthors).ThenInclude(a => a.Author).ToListAsync();

            List<BookPreviewDTO> booksPreviews = [];
            int i = 0;
            foreach (var book in books)
            {
                booksPreviews.Add(new BookPreviewDTO() 
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = $"{book.BookAuthors.First().Author.FirstName} {book.BookAuthors.First().Author.MiddleName} {book.BookAuthors.First().Author.LastName}",
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price
                });
            }

            // TODO Take only values u need 
            //BookPreviewDTO booksPreviews = new BookPreviewDTO();

            return booksPreviews;
        }

        public async Task<Book> GetBookDetailsAsync(int id)
        {
            var book = await _dbContext.Books.Include(a => a.Publisher).Include(a => a.BookAuthors).ThenInclude(a => a.Author).FirstOrDefaultAsync(b => b.Id == id) ?? throw new ArgumentNullException($"No book found with id: {id}");
/*           _dbContext.Entry(book)
                .Collection(b => b.BookAuthors)
                .Query()
                .Include(b => b.Author)
                .Where(a => a.AuthorId == id)
                .Load();*/

            return book;
        }

        public async Task<Book> CreateBookA(int authorId, Book book)
        {
            var bookAuthorEntity = await _dbContext.Authors.Where(a => a.Id == authorId).FirstOrDefaultAsync();

            var bookAuthor = new BookAuthor()
            {
                Author = bookAuthorEntity,
                Book = book,
            };

            _dbContext.Add(bookAuthor);

            _dbContext.Add(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<Book> CreateBookAP(CreateBookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }


        /* TO Change maybee
public async Task<IEnumerable<BookDTO>> GetAllAsync2()
{
  var book = _dbContext
      .Books
      .Include(b => b.Publisher)
      //.Include(b => b.BookAuthors)
      .ToListAsync();

  var bookDTO = _mapper.Map<List<BookDTO>>(book);
  return bookDTO;
}
*/


        /* public async Task<IEnumerable<Book>> GetBooksAsync()
         {
             return await _dbContext.Books.ToListAsync();
         }

         public async Task<Book> GetBookByIdAsync(int id, bool useNoTracking = false)
         {
             if (useNoTracking)
                 return await _dbContext.Books.AsNoTracking().Where(book => book.Id == id).FirstOrDefaultAsync();

             else
                 return await _dbContext.Books.Where(book => book.Id == id).FirstOrDefaultAsync();


         }

         public async Task<Book> GetBookByTitleAsync(string title)
         {
             return await _dbContext.Books.Where(book => book.Title.ToLower().Contains(title.ToLower())).FirstOrDefaultAsync();
         }



         public async Task<int> CreateBookAsync(Book book)
         {
             _dbContext.Books.Add(book);
             await _dbContext.SaveChangesAsync();
             return book.Id;
         }

         public async Task<Book> UpdateBookAsync(Book book)
         {
             *//*var bookUpdate = await _dbContext.Books.Where(book => book.Id == book.Id).FirstOrDefaultAsync() ?? throw new ArgumentNullException($"No book found with id: {book.Id}");

             bookUpdate.Title = book.Title;
             bookUpdate.PhotoUrl = book.PhotoUrl;
             bookUpdate.Description = book.Description;
             bookUpdate.Pages = book.Pages;
             bookUpdate.PublisherId = book.PublisherId;
             bookUpdate.Price = book.Price;
             bookUpdate.Rate = book.Rate;
             bookUpdate.Series = book.Series;
             bookUpdate.PublicationDate = book.PublicationDate;
             bookUpdate.Language = book.Language;*//*
             _dbContext.Update(book);
             await _dbContext.SaveChangesAsync();

             return book;
         }
         public async Task<bool> DeleteBookAsync(Book book)
         {
             _dbContext.Books.Remove(book);
             await _dbContext.SaveChangesAsync();
             return true;
         }*/
    }
}
