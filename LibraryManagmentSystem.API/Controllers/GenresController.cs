using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Domain.DTOs.GenreDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var response = await _genreService.GetAllGenresAsync();
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var response = await _genreService.GetGenreByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response.Message);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGenre([FromBody] GenreDto addGenreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _genreService.AddGenreAsync(addGenreDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetGenreById), new { id = response.Data.Id }, response.Data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDto updateGenreDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _genreService.UpdateGenreAsync(id, updateGenreDto);
            return response.Success ? Ok(response) : NotFound(response.Message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var response = await _genreService.DeleteGenreAsync(id);
            return response.Success ? Ok(response) : NotFound(response.Message);
        }
    }
}
