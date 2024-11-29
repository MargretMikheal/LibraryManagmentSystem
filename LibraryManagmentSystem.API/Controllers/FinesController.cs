using LibraryManagmentSystem.Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinesController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FinesController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpGet("GetAllFines")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllFines()
        {
            var response = await _fineService.GetAllFinesAsync();
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFineById(int id)
        {
            var response = await _fineService.GetFineByIdAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpGet("UserFines/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetFinesByUserId(string userId)
        {
            var response = await _fineService.GetFinesByUserIdAsync(userId);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        [HttpPut("MarkAsPaid/{id}")]
        [Authorize]
        public async Task<IActionResult> MarkFineAsPaid(int id)
        {
            var response = await _fineService.MarkFineAsPaidAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Message);
        }
    }

}
