using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sever.DTO;
using Sever.Models;

namespace Sever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly DBContext _context;

        public PublishersController(DBContext context)
        {
            _context = context;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static PublisherResponse PublisherToResponse(Publisher publisher)
        {
            return new PublisherResponse()
            {
                city = publisher.city,
                country = publisher.country,
                publisher_name = publisher.publisher_name,
                pub_id = publisher.pub_id,
                state = publisher.state

            };
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublisher()
        {
            if (_context.Publisher == null)
            {
                return NotFound();
            }
            return await _context.Publisher.ToListAsync();
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            if (_context.Publisher == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publisher.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherRequest req)
        {

            var publisher = await _context.Publisher.SingleOrDefaultAsync(p => p.pub_id == id);
            if (publisher == null)
            {
                return BadRequest();
            }
            publisher.publisher_name = req.publisher_name;
            publisher.city = req.city;
            publisher.state = req.state;
            publisher.country = req.country;

            _context.Update(publisher);

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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(PublisherRequest req)
        {
            if (_context.Publisher == null)
            {
                return Problem("Entity set 'DBContext.Publisher'  is null.");
            }
            var publisher = new Publisher()
            {
                publisher_name = req.publisher_name,
                city = req.city,
                state = req.state,
                country = req.country,
            };
            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

            return Ok(PublisherToResponse(publisher));
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            if (_context.Publisher == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publisher.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publisher?.Any(e => e.pub_id == id)).GetValueOrDefault();
        }
    }
}
