using BookStore.Application.DTOs.Book;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book is null)
        {
            return NotFound(new
            {
                Message = $"كتاب برقم {id} غير موجود"
            });
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBook model)
    {
        var createdBook = await _bookService.CreateBookAsync(model);

        return CreatedAtAction(
            nameof(GetBookById),
            new { id = createdBook.Id },
            createdBook);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] CreateBook model)
    {
        var updated = await _bookService.UpdateBookAsync(id, model);

        if (!updated)
        {
            return NotFound(new
            {
                Message = $"كتاب برقم {id} غير موجود"
            });
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteBookAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                Message = $"كتاب برقم {id} غير موجود"
            });
        }

        return NoContent();
    }


}
