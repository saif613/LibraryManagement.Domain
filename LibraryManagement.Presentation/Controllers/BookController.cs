using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1)
        {
            var result = await _bookService.GetBooksPagedAsync(pageNumber);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookById(int id, CancellationToken ct)
        {
            var book = await _bookService.GetBookByIdAsync(id, ct);
            return Ok(book);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook(CreateBookRequest request, CancellationToken ct)
        {
            var createdBook = await _bookService.CreateBookAsync(request, ct);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(UpdateBookMetadataRequest request, CancellationToken ct)
        {
            await _bookService.UpdateBookAsync(request, ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDeleteBook(int id, CancellationToken ct)
        {
            await _bookService.SoftDeleteBookAsync(id, ct);
            return NoContent();
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchBooksByItem([FromQuery] string item, CancellationToken ct)
        {
            var books = await _bookService.SearchBookByItemAsyncForBook(item, ct);
            return Ok(books);
        }
    }
}
