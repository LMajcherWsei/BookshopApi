using BookshopApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookshopApi.Repository
{
    public class BookshopRepository<T> : IBookshopRepository<T> where T : class
    {
        private readonly BooksContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BookshopRepository(BooksContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }

/*        public async Task<T> GetBookByTitleAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }*/

/*        public async Task<T> GetBookDetailsAsync(Expression<Func<T, bool>> filter)
        {
            var book = await _dbSet.SingleAsync(filter) ?? throw new ArgumentNullException($"No book found with id: {filter}");

            _dbContext.Entry(book)
                .Collection(b => b.BookAuthors)
                .Query()
                .Include(b => b.Author)
                .Where(a => a.AuthorId == id)
                .Load();

            return book;
        }*/

        public async Task<T> UpdateAsync(T dbRecord)
        {
            _dbSet.Update(dbRecord);
            await _dbContext.SaveChangesAsync();

            return dbRecord;
        }

        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
