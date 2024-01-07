using Microsoft.AspNetCore.Mvc;
using LiteraLog.Models;
using Microsoft.EntityFrameworkCore;

namespace LiteraLog.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            return await _context.Books.ToListAsync();
        }

        // Responds to GET api/BookAPI/{id}
        [HttpGet("all/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (id == 0 || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost("all/new")]
        public async Task<ActionResult<Book>> PostBook (Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("all/update")]
        public async Task<ActionResult<Book>> PutBook (Book book)
        {
            if (book.Id == 0)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == book.Id))
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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Book>> DeleteBook (int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
