using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("copies")]
    public class Copies : ControllerBase
    {
        private readonly LibraryContext _context;

        public Copies(LibraryContext context)
        {
            _context = context;
        }

        // GET /copies
        [HttpGet]
        public async Task<IActionResult> GetCopies()
        {
            return Ok(await _context.Copies.ToListAsync());
        }

        // GET /copies/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCopy(int id)
        {
            var copy = await _context.Copies.FindAsync(id);
            if (copy == null)
                return NotFound();

            return Ok(copy);
        }

        // POST /copies
        [HttpPost]
        public async Task<IActionResult> CreateCopy(Copy copy)
        {
            var bookExists = await _context.Books.AnyAsync(b => b.Id == copy.BookId);
            if (!bookExists)
                return BadRequest();

            _context.Copies.Add(copy);
            await _context.SaveChangesAsync();

            return Ok(copy);
        }

        // PUT /copies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCopy(int id, Copy copy)
        {
            if (id != copy.Id)
                return BadRequest();

            var bookExists = await _context.Books.AnyAsync(b => b.Id == copy.BookId);
            if (!bookExists)
                return BadRequest();

            _context.Entry(copy).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /copies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCopy(int id)
        {
            var copy = await _context.Copies.FindAsync(id);
            if (copy == null)
                return NotFound();

            _context.Copies.Remove(copy);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
