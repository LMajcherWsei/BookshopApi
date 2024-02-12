using BookshopApi.Data;
using BookshopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookshopApi.Repository
{
    public class PublisherRepository : BookshopRepository<Publisher>, IPublisherRepository
    {
        private readonly BooksContext _dbContext;
        public PublisherRepository(BooksContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
