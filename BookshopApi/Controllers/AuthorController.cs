using AutoMapper;
using BookshopApi.Data;
using BookshopApi.DTO;
using BookshopApi.Models;
using BookshopApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _repository;

        public AuthorController(ILogger<AuthorController> logger, IMapper mapper, IAuthorRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var author = await _repository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorDTO = _mapper.Map<List<AuthorDTO>>(author);

            return Ok(authorDTO);
        }

        [HttpGet("{id:int}", Name = "GetAuthorById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }

            var author = await _repository.GetAsync(author => author.Id == id);
            if (author == null)
            {
                _logger.LogError("Author not founded with given Id");
                return NotFound($"The author with id: {id} not found");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorDTO = _mapper.Map<AuthorDTO>(author);

            return Ok(authorDTO);
        }

        [HttpGet("{lastName:alpha}", Name = "GetAuthorByLastName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDTO>> GetAuthorByLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return BadRequest();

            var author = await _repository.GetAsync(author => author.LastName.ToLower().Contains(lastName.ToLower()));
            if (author == null)
                return NotFound($"The author with id {lastName} not found");

            var authorDTO = _mapper.Map<AuthorDTO>(author);

            return Ok(authorDTO);
        }

        [HttpPost("Create", Name = "CreateAuthor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (authorDTO == null)
                return BadRequest();

            var author = _mapper.Map<Author>(authorDTO);

            var createdAuthor = await _repository.CreateAsync(author);

            authorDTO.Id = createdAuthor.Id;
            return CreatedAtRoute("GetAuthorById", new { id = authorDTO.Id }, authorDTO);
        }

        [HttpPut("Update", Name = "UpdateAuthor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (authorDTO == null || authorDTO.Id <= 0)
                BadRequest();

            var existingAuthor = await _repository.GetAsync(author => author.Id == authorDTO.Id, true);
            if (existingAuthor == null)
                return NotFound();

            var newRecord = _mapper.Map<Author>(authorDTO);
            _repository.UpdateAsync(newRecord);

            return NoContent();
        }

        [HttpPatch("{id}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAuthorPartial(int id, [FromBody] JsonPatchDocument<AuthorDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingAuthor = await _repository.GetAsync(author => author.Id == id, true);

            if (existingAuthor == null)
                return NotFound();

            var authorDTO = _mapper.Map<AuthorDTO>(existingAuthor);
            patchDocument.ApplyTo(authorDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingAuthor = _mapper.Map<Author>(authorDTO);

            await _repository.UpdateAsync(existingAuthor);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteAuthor(int id)
        {
            if (id <= 0)
                return BadRequest();

            var author = await _repository.GetAsync(author => author.Id == id);

            if (author == null)
                return NotFound($"The author with id {id} not found");

            await _repository.DeleteAsync(author);

            return Ok(true);
        }
    }
}
