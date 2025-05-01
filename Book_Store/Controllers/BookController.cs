using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Services;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        // Accessible by both Admin and User
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks()
        {
            var books = await _service.GetAllBooksAsync();
            return Ok(books);
        }

        // Accessible by both Admin and User
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<BookModel>> GetBook(int id)
        {
            var book = await _service.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        // Only Admin can add books
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookModel>> CreateBook(BookModel book)
        {
            var created = await _service.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = created.Id }, created);
        }

        // Only Admin can update books
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, BookModel book)
        {
            var updated = await _service.UpdateBookAsync(id, book);
            return updated == null ? NotFound() : Ok(updated);
        }

        // Only Admin can delete books
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid book ID." });
            }

            var success = await _service.DeleteBookAsync(id);
            if (GetBooks == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found." });
            }

            await _service.DeleteBookAsync(id);
            return Ok(new { message = $"Book with ID {id} deleted successfully." });
        }
    }
}
