using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Domain.DTOs.BorrowingDTos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BorrowingsController : ControllerBase
{
    private readonly IBorrowingService _borrowingService;

    public BorrowingsController(IBorrowingService borrowingService)
    {
        _borrowingService = borrowingService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllBorrowings()
    {
        var response = await _borrowingService.GetAllBorrowingsAsync();
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    [HttpGet("{id}")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> GetBorrowingById(int id)
    {
        var response = await _borrowingService.GetBorrowingByIdAsync(id);
        return response.Success ? Ok(response.Data) : NotFound(response.Message);
    }

    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetBorrowingsByUser(string userId)
    {
        var response = await _borrowingService.GetBorrowingsByUserAsync(userId);
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingDto createBorrowingDto)
    {
        var response = await _borrowingService.CreateBorrowingAsync(createBorrowingDto);
        return response.Success ? CreatedAtAction(nameof(GetBorrowingById), new { id = response.Data.Id }, response.Data) : BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var response = await _borrowingService.ReturnBookAsync(id);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }
}
