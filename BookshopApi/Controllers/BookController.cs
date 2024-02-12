using AutoMapper;
using BookshopApi.Data;
using BookshopApi.DTO;
using BookshopApi.Models;
using BookshopApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class BookController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        //private readonly IBookRepository _repository;
        private readonly IBookRepository _repository;

        //public BookController(ILogger<BookController> logger, IMapper mapper, IBookshopRepository<Book> repository)
        public BookController(ILogger<BookController> logger, IMapper mapper, IBookRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet(Name = "GetAllBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            _logger.LogInformation("GetBooks method started");
            var books =  await _repository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDTO = _mapper.Map<List<BookDTO>>(books);

            return Ok(bookDTO);
        }

        [HttpGet("{id:int}", Name = "GetBookById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }

            //var book = await _repository.GetBookByIdAsync(id);
            var book = await _repository.GetAsync(book => book.Id == id);
            if (book == null)
            {
                _logger.LogError("Book not founded with given Id");
                return NotFound($"The book with id: {id} not found");
            }
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDTO = _mapper.Map<BookDTO>(book);

            return Ok(bookDTO);
        }

        [HttpGet("GetBookDetails/{id}")]
        public async Task<ActionResult<Book>> GetBookDetails(int id)
        {
            var book = await _repository.GetBookDetailsAsync(id);
            //var book = await _repository.GetBookDetailsAsync(book => book.Id == id);

            await _repository.GetBookDetailsAsync(book.Id);
            _mapper.Map<PublisherDTO>(book.Publisher);

            if (book == null)
               return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return book;
        }


        [HttpGet("{title:alpha}", Name = "GetBookByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDTO>> GetBookByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest();

            //var book = await _repository.GetBookByTitleAsync(title);
            var book = await _repository.GetAsync(book => book.Title.ToLower().Contains(title.ToLower()));
            if (book == null)
                return NotFound($"The book with id {title} not found");
            
            var bookDTO = _mapper.Map<BookDTO>(book);

            return Ok(bookDTO);
        }


        [HttpPost("Create", Name = "CreateBook")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookDTO bookDTO)
        {
            if (bookDTO == null)
                return BadRequest();

            Book book = _mapper.Map<Book>(bookDTO);

            var createdBook = await _repository.CreateAsync(book);

            bookDTO.Id = createdBook.Id;
            return CreatedAtRoute("GetBookById", new { id = bookDTO.Id }, bookDTO);
        }

        [HttpPut("Update", Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBook([FromBody] BookDTO bookDTO)
        {
            if (bookDTO == null || bookDTO.Id <= 0)
                BadRequest();
            // No tracking to use other object with id
            //var existingBook = await _dbContext.Books.Where(b => b.Id == bookDTO.Id).FirstOrDefaultAsync();
            //var existingBook = await _dbContext.Books.AsNoTracking().Where(b => b.Id == bookDTO.Id).FirstOrDefaultAsync();
            
            
            //var existingBook = await _repository.GetBookByIdAsync(bookDTO.Id, true);
            var existingBook = await _repository.GetAsync(book => book.Id == bookDTO.Id, true);
            if (existingBook == null)
                return NotFound();

            //var newer = _mapper.Map<BookDTO>(existingBook);
            var newRecord = _mapper.Map<Book>(bookDTO);
            _repository.UpdateAsync(newRecord);

            return NoContent();
        }

        [HttpPatch("{id}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBookPartial(int id, [FromBody] JsonPatchDocument<BookDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            //var existingBook = await _repository.GetBookByIdAsync(id, true);
            var existingBook = await _repository.GetAsync(book => book.Id == id, true);

            if (existingBook == null)
                return NotFound();

            var bookDTO = _mapper.Map<BookDTO>(existingBook);
            patchDocument.ApplyTo(bookDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingBook = _mapper.Map<Book>(bookDTO);

            await _repository.UpdateAsync(existingBook);

            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteBookById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            if (id <= 0)
                return BadRequest();

            //var book = await _repository.GetBookByIdAsync(id);
            var book = await _repository.GetAsync(book => book.Id == id);
            if (book == null)
                return NotFound($"The book with id {id} not found");

            await _repository.DeleteAsync(book);

            return Ok(true);
        }

    }
}
