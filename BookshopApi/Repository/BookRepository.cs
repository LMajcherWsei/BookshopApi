using AutoMapper;
using BookshopApi.Controllers;
using BookshopApi.Data;
using BookshopApi.DTO;
using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopApi.Repository
{
    public class BookRepository : BookshopRepository<Book>, IBookRepository
    {
        private readonly BooksContext _dbContext;

        public BookRepository(BooksContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> GetBookDetailsAsync(int id)
        {
            var book = await _dbContext.Books.SingleAsync(b => b.Id == id) ?? throw new ArgumentNullException($"No book found with id: {id}");

            _dbContext.Entry(book)
                .Collection(b => b.BookAuthors)
                .Query()
                .Include(b => b.Author)
                .Where(a => a.AuthorId == id)
                .Load();

            return book;
        }


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
