using BookshopApi.Data;
using BookshopApi.Models;

namespace BookshopApi.Repository
{
    public class AuthorRepository : BookshopRepository<Author>, IAuthorRepository
    {
        private readonly BooksContext _dbContext;
        public AuthorRepository(BooksContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
