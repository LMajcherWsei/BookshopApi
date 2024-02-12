using BookshopApi.Data;
using BookshopApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BooksContext _dbContext;

        public AuthorController(BooksContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Author> Get()
        {
                return _dbContext.Authors.ToList();
        }

        [HttpGet("{id:int}")]
        public IEnumerable<Author> GetAuthor(int id)
        {
            return _dbContext.Authors.Where(author => author.Id == id).ToList();
        }

        [HttpPut("{id:int}")]
        public IEnumerable<Author> CreateAuthor(int id)
        {
            Author author = new Author();
            return _dbContext.Authors.Where(author => author.Id == id).ToList();
        }

    }
}
