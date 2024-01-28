using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sever.DTO;
using Sever.Models;

namespace Sever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DBContext _context;

        public AuthorsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var data = await _context.Authors
                  .Select(p => new AuthorResponse()
                  {
                      city = p.city,
                      email_address = p.email_address,
                      first_name = p.first_name,
                      last_name = p.last_name,
                      author_id = p.author_id,
                  })
                  .ToListAsync();
            return Ok(data);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            var data = new AuthorResponse()
            {
                last_name = author.last_name,
                first_name = author.first_name,
                email_address = author.email_address,
                city = author.city,
                author_id = author.author_id
            };

            if (author == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorRequest authorRequest)
        {


            var author = _context.Authors.Find(id);
            if (author != null)
            {
                author.first_name = authorRequest.first_name;
                author.last_name = authorRequest.last_name;
                author.email_address = authorRequest.email_address;
                author.city = authorRequest.city;

                _context.Update(author);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorRequest request)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'DBContext.Authors'  is null.");
            }
            var author = new Author
            {
                first_name = request.first_name,
                last_name = request.last_name,
                email_address = request.email_address,
                city = request.city,
            };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.author_id == id)).GetValueOrDefault();
        }
    }
}
