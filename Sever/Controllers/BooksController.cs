using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sever.DTO;
using Sever.Models;

namespace Sever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DBContext _context;

        public BooksController(DBContext context)
        {
            _context = context;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public static BookResponse ConvertBookResponse(Book p)
        {
            return new BookResponse()
            {
                book_id = p.book_id,
                notes = p.notes,
                price = p.price,
                publisher_date = p.publisher_date,
                pub_id = p.pub_id,
                title = p.title,
                type = p.type,
                PublisherResponse = new PublisherResponse
                {
                    city = p.Publisher.city,
                    country = p.Publisher.country,
                    publisher_name = p.Publisher.publisher_name,
                    pub_id = p.Publisher.pub_id
                },
                BookAuthorResponse = p.BookAuthors.Select(o => new BookAuthorResponse()
                {
                    author_id = o.author_id,
                    author_order = o.author_order,
                    book_id = o.book_id,
                    royality_percentage = o.royality_percentage
                }).ToList()
            };
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public static BookRequest ConverBookRequest(Book book)
        {
            return new BookRequest()
            {
                type = book.type,
                title = book.title,
                publisher_date = book.publisher_date,
                notes = book.notes,
                price = book.price,
            };
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var data = await _context.Books
                  .Include(p => p.Publisher)
                  .Include(p => p.BookAuthors)
                  .Select(p => ConvertBookResponse(p))
                  .ToListAsync();
            return Ok(data);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books
                .Include(p => p.BookAuthors)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(p => p.book_id == id);
            var res = ConvertBookResponse(book);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookRequest req)
        {
            var book = _context.Books.Find(id);
            book.notes = req.notes;
            book.price = req.price;
            book.title = req.title;
            book.publisher_date = req.publisher_date;
            book.price = req.price;

            _context.Update(book);
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookRequest req)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'DBContext.Books'  is null.");
            }
            var book = new Book()
            {
                notes = req.notes,
                price = req.price,
                publisher_date = req.publisher_date,
                title = req.title,
                type = req.type,
                pub_id = req.pub_id,
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.book_id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Title/{title}")]
        public async Task<ActionResult<BookResponse>> GetBookByTitle(string title)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var books = await _context.Books
                .Include(p => p.BookAuthors)
                .Include(p => p.Publisher)
                .Where(p => p.title.ToLower().Contains(title.ToLower()))
                .Select(p => ConvertBookResponse(p))
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("Price/{price}")]
        public async Task<ActionResult<BookResponse>> GetBookByPrice(float price)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var books = await _context.Books
                .Include(p => p.BookAuthors)
                .Include(p => p.Publisher)
                .Where(p => p.price == price)
                .Select(p => ConvertBookResponse(p))
                .ToListAsync();

            return Ok(books);
        }
    }
}
