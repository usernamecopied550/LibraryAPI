using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET /authors
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _context.Authors.ToListAsync());
        }

        // GET /authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST /authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.First_Name) ||
                string.IsNullOrWhiteSpace(author.Last_Name))
            {
                return BadRequest();
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        // PUT /authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(author.First_Name) ||
                string.IsNullOrWhiteSpace(author.Last_Name))
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
