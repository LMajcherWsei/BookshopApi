using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookshopApi.Data;
using BookshopApi.Models;
using AutoMapper;
using BookshopApi.Repository;
using BookshopApi.DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace BookshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPublisherRepository _repository;

        public PublisherController(ILogger<PublisherController> logger, IMapper mapper, IPublisherRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Publishers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetPublishers()
        {
            var publishers = await _repository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publishersDTO = _mapper.Map<List<PublisherDTO>>(publishers);

            return Ok(publishersDTO);
        }

        // GET: api/Publishers/5
        [HttpGet("{id:int}", Name = "GetPublisherById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublisherDTO>> GetPublisherById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }

            var publisher = await _repository.GetAsync(publisher => publisher.Id == id);

            if (publisher == null)
            {
                _logger.LogError("Publisher not founded with given Id");
                return NotFound($"The publisher with id: {id} not found");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherDTO = _mapper.Map<PublisherDTO>(publisher);

            return Ok(publisherDTO);
        }

        [HttpGet("{publisherName:alpha}", Name = "GetPublisherByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublisherDTO>> GetPublisherByName(string publisherName)
        {
            if (string.IsNullOrEmpty(publisherName))
                return BadRequest();

            var publisher = await _repository.GetAsync(publisher => publisher.PublisherName.ToLower().Contains(publisherName.ToLower()));
            if (publisher == null)
                return NotFound($"The publisher with id {publisherName} not found");

            var publisherDTO = _mapper.Map<PublisherDTO>(publisher);

            return Ok(publisherDTO);
        }

        //TODO Make it in PublisherRepo
        // GET: api/Publishers/5
        /*       [HttpGet("GetPublisherDetails/{id}")]
               public async Task<ActionResult<Publisher>> GetPublisherDetails(int id)
               {
       *//*              Eager Loading
                        var publisher = _context.Publishers
                       .Include(pub => pub.Books)
                       .Where(pub => pub.Id == id)
                       .FirstOrDefault();*//*

                   var publisher = await _context.Publishers.SingleAsync(pub => pub.Id == id);

                   _context.Entry(publisher)
                       .Collection(pub => pub.Books)
                       .Load();

                   if (publisher == null)
                   {
                       return NotFound();
                   }

                   return publisher;
               }*/


        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update", Name = "UpdatePublisher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePublisher([FromBody] PublisherDTO publisherDTO)
        {
            if (publisherDTO == null || publisherDTO.Id <= 0)
                BadRequest();

            var existingPublisher = await _repository.GetAsync(publisher => publisher.Id == publisherDTO.Id, true);
            
            if (existingPublisher == null)
                return NotFound();

            var newRecord = _mapper.Map<Publisher>(publisherDTO);
            _repository.UpdateAsync(newRecord);

            return NoContent();
        }

        /*     
         *     [HttpPut("{id}")]
                public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
                {
                    if (id != publisher.Id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(publisher).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PublisherExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return NoContent();
                }
        */

        [HttpPatch("{id}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePublisherPartial(int id, [FromBody] JsonPatchDocument<PublisherDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingPublisher = await _repository.GetAsync(publisher => publisher.Id == id, true);

            if (existingPublisher == null)
                return NotFound();

            var publisherDTO = _mapper.Map<PublisherDTO>(existingPublisher);
            patchDocument.ApplyTo(publisherDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingPublisher = _mapper.Map<Publisher>(publisherDTO);

            await _repository.UpdateAsync(existingPublisher);

            return NoContent();
        }

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "CreatePublisher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublisherDTO>> CreatePublisher([FromBody] PublisherDTO publisherDTO)
        {
            if (publisherDTO == null)
                return BadRequest();

            var publisher = _mapper.Map<Publisher>(publisherDTO);

            var createdPublisher = await _repository.CreateAsync(publisher);

            publisherDTO.Id = createdPublisher.Id;
            return CreatedAtRoute("GetPublisherById", new { id = publisherDTO.Id }, publisherDTO);
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}", Name = "DeletePublisher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeletePublisher(int id)
        {
            if (id <= 0)
                return BadRequest();

            var publisher = await _repository.GetAsync(publisher => publisher.Id == id);

            if (publisher == null)
                return NotFound($"The publisher with id {id} not found");

            await _repository.DeleteAsync(publisher);

            return Ok(true);
        }

/*        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }*/
    }
}
