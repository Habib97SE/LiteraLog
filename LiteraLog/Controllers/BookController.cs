using Microsoft.AspNetCore.Mvc;
using LiteraLog.Models;
using Microsoft.EntityFrameworkCore;
using LiteraLog.Models.DTOs;
using LiteraLog.Models.DTOs.Books;

namespace LiteraLog.Controllers
{
    [Route("api/books/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var books = await _context.Books.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Author = b.Author,
                CoverImage = b.CoverImage,
                ISBN = b.ISBN,
                Pages = b.Pages,
                Category = b.Category,
                Language = b.Language,
                Rating = b.Rating,
                Comment = b.Comment,
                PublishedYear = b.PublishedYear,
                UserId = b.UserId
            }).ToListAsync();
            return Ok(books);
        }

        // Responds to GET api/BookAPI/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (_context.Books == null || id <= 0)
            {
                return BadRequest();
            }
            var book = await _context.Books
                             .Where(b => b.Id == id)
                             .Select(b => new BookDTO
                             {
                                 Id = b.Id,
                                 Title = b.Title,
                                 Description = b.Description,
                                 Author = b.Author,
                                 CoverImage = b.CoverImage,
                                 ISBN = b.ISBN,
                                 Pages = b.Pages,
                                 Category = b.Category,
                                 Language = b.Language,
                                 Rating = b.Rating,
                                 Comment = b.Comment,
                                 PublishedYear = b.PublishedYear,
                                 UserId = b.UserId
                             })
                             .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> PostBook(BookCreationDTO bookCreationDTO)
        {
            var book = new Book
            {
                Title = bookCreationDTO.Title,
                Description = bookCreationDTO.Description,
                Author = bookCreationDTO.Author,
                CoverImage = bookCreationDTO.CoverImage,
                ISBN = bookCreationDTO.ISBN,
                Pages = bookCreationDTO.Pages,
                Category = bookCreationDTO.Category,
                Language = bookCreationDTO.Language,
                Rating = bookCreationDTO.Rating,
                Comment = bookCreationDTO.Comment,
                PublishedYear = bookCreationDTO.PublishedYear,
                UserId = bookCreationDTO.UserId
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var bookDTO = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                CoverImage = book.CoverImage,
                ISBN = book.ISBN,
                Pages = book.Pages,
                Category = book.Category,
                Language = book.Language,
                Rating = book.Rating,
                Comment = book.Comment,
                PublishedYear = book.PublishedYear,
                UserId = book.UserId
            };
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDTO);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDTO>> PutBook(int id, BookUpdateDTO bookUpdateDTO)
        {
            if (id <= 0 || bookUpdateDTO == null)
            {
                return BadRequest();
            } 


            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = bookUpdateDTO.Title;
            book.Description = bookUpdateDTO.Description;
            book.Author = bookUpdateDTO.Author;
            book.ISBN = bookUpdateDTO.ISBN;
            book.CoverImage = bookUpdateDTO.CoverImage;
            book.Pages = bookUpdateDTO.Pages;
            book.Category = bookUpdateDTO.Category;
            book.Language = bookUpdateDTO.Language;
            book.Rating = bookUpdateDTO.Rating;
            book.Comment = bookUpdateDTO.Comment;
            book.PublishedYear = bookUpdateDTO.PublishedYear;
            book.UserId = bookUpdateDTO.UserId;
            book.LastUpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(bookUpdateDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return NotFound(nameof(Book));
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BookDTO>> DeleteBook(int id)
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
