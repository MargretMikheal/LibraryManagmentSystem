using LibraryManagmentSystem.Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagmentSystem.Domain.DTOs.BookDTOs;

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
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _bookService.GetAllBooksAsync();
        if (!response.Success) return BadRequest(response.Message);

        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        if (!response.Success) return NotFound(response.Message);

        return Ok(response.Data);
    }

    [HttpGet("by-genre/{genreId}")]
    public async Task<IActionResult> GetBooksByGenre(int genreId)
    {
        var response = await _bookService.GetBooksByGenreAsync(genreId);
        if (!response.Success) return BadRequest(response.Message);

        return Ok(response.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _bookService.AddBookAsync(addBookDto);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return CreatedAtAction(nameof(GetBookById), new { id = response.Data.Id }, response.Data);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var response = await _bookService.UpdateBookAsync(id, updateBookDto);
        if (!response.Success) return BadRequest(response.Message);

        return Ok(response.Message);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await _bookService.DeleteBookAsync(id);
        if (!response.Success) return NotFound(response.Message);

        return Ok(response.Message);
    }
}