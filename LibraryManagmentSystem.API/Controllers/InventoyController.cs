
using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Domain.DTOs.InventoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var response = await _inventoryService.GetAllInventoriesAsync();
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetInventoryByBookId(int bookId)
        {
            var response = await _inventoryService.GetInventoryByBookIdAsync(bookId);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateInventory(int bookId, [FromBody] UpdateInventoryDto updateInventoryDto)
        {
            var response = await _inventoryService.UpdateInventoryAsync(bookId, updateInventoryDto);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}
