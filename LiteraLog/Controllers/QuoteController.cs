using LiteraLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraLog.Controllers
{
    [Route("api/quotes")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            if (_context.Quotes == null)
            {
                return NotFound();
            }
            return await _context.Quotes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            if (id == 0 || _context.Books == null)
            {
                return NotFound();
            }
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            return quote;
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote([FromBody]Quote quote)
        {
            if (quote == null) 
            {
                return BadRequest(quote);
            } 
            if (quote.Id > 0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            } 
            _context.Quotes.Add(quote);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Quote>> PutQuote(int id)
        {
            if (id <= 0) { return BadRequest(); }

            Quote quote = await _context.Quotes.FindAsync(id); 
            if (quote == null)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == quote.Id))
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Quote>> DeleteQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
