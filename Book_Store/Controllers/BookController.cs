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
    public class bookController : ControllerBase
    {
        private readonly IBookService _service;

        public bookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _service.GetAllBooksAsync();
            return Ok(new ResponseModel<IEnumerable<BookModel>>
            {
                success = true,
                message = "Books retrieved successfully.",
                data = books
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _service.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new ResponseModel<BookModel>
                {
                    success = false,
                    message = $"Book with ID {id} not found.",
                    data = null
                });
            }

            return Ok(new ResponseModel<BookModel>
            {
                success = true,
                message = "Book retrieved successfully.",
                data = book
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook(BookModel book)
        {
            var created = await _service.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = created.Id }, new ResponseModel<BookModel>
            {
                success = true,
                message = "Book created successfully.",
                data = created
            });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, BookModel book)
        {
            var updated = await _service.UpdateBookAsync(id, book);
            if (updated == null)
            {
                return NotFound(new ResponseModel<BookModel>
                {
                    success = false,
                    message = $"Book with ID {id} not found.",
                    data = null
                });
            }

            return Ok(new ResponseModel<BookModel>
            {
                success = true,
                message = "Book updated successfully.",
                data = updated
            });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ResponseModel<string>
                {
                    success = false,
                    message = "Invalid book ID.",
                    data = null
                });
            }

            var success = await _service.DeleteBookAsync(id);
            if (!success)
            {
                return NotFound(new ResponseModel<string>
                {
                    success = false,
                    message = $"Book with ID {id} not found.",
                    data = null
                });
            }

            return Ok(new ResponseModel<string>
            {
                success = true,
                message = $"Book with ID {id} deleted successfully.",
                data = null
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string? author)
        {
            var books = await _service.SearchBooksAsync(author);
            return Ok(new ResponseModel<IEnumerable<BookModel>>
            {
                success = true,
                message = "Search completed.",
                data = books
            });
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortBooksByPrice([FromQuery] string price)
        {
            try
            {
                var books = await _service.SortBooksByPriceAsync(price);
                return Ok(new ResponseModel<IEnumerable<BookModel>>
                {
                    success = true,
                    message = $"Books sorted by {price} price.",
                    data = books
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    success = false,
                    message = ex.Message,
                    data = null
                });
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
            return Ok(new ResponseModel<IEnumerable<BookModel>>
            {
                success = true,
                message = $"Books for page {pageNumber} retrieved successfully.",
                data = books
            });
        }


    }
}
