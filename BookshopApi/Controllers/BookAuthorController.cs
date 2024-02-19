using BookshopApi.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookRepository bookRepository;

        /*
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBookAuthor()
        {
            var book = await 
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
        */
    }
}
