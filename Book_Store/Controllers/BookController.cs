using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Services;
using DataAccess_Layer.DTO_s;
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? author)
        {
            var books = await _service.SearchBooksAsync( author);
            return Ok(books);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortBooksByPrice([FromQuery] string price)
        {
            try
            {
                var books = await _service.SortBooksByPriceAsync(price);
                return Ok(books);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("pagination")]
        public IActionResult GetBooksByPageNumber([FromQuery] int pageNumber)
        {
            if (pageNumber < 1)
            {
                return BadRequest(new ResponseModel<string>
                {
                    success = false,
                    message = "Page number must be greater than 0.",
                    data = null
                });
            }

            var books = _service.GetBooksByPageNumber(pageNumber);

            return Ok(new ResponseModel<List<BookModel>>
            {
                success = true,
                message = $"Books for page {pageNumber} retrieved successfully.",
                data = books
            });
        }


    }
}
