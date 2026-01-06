using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET /books?authorId=1
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] int? authorId)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (authorId.HasValue)
                query = query.Where(b => b.AuthorId == authorId.Value);

            var books = await query.Select(b => new BookReadDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Author = b.Author
            }).ToListAsync();

            return Ok(books);
        }

        // GET /books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                Author = book.Author
            });
        }

        // POST /books
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Year < 0)
                return BadRequest();

            var author = await _context.Authors.FindAsync(dto.AuthorId);
            if (author == null)
                return BadRequest();

            var book = new Book
            {
                Title = dto.Title,
                Year = dto.Year,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            book.Author = author;

            return Ok(new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                Author = author
            });
        }

        // PUT /books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookCreateDto dto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Year < 0)
                return BadRequest();

            var author = await _context.Authors.FindAsync(dto.AuthorId);
            if (author == null)
                return BadRequest();

            book.Title = dto.Title;
            book.Year = dto.Year;
            book.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
            book.Author = author;

            return Ok(new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                Author = author
            });
        }

        // DELETE /books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
