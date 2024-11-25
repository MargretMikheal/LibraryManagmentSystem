using LibraryManagmentSystem.Domain.DTOs.GenreDtos;


namespace LibraryManagmentSystem.Core.Interfaces.Service
{ 
    public interface IGenreService
    {
        Task<ServiceResponse<List<GetGenreDto>>> GetAllGenresAsync();
        Task<ServiceResponse<GetGenreDto>> GetGenreByIdAsync(int id);
        Task<ServiceResponse<GetGenreDto>> AddGenreAsync(GenreDto createGetGenreDto);
        Task<ServiceResponse<GetGenreDto>> UpdateGenreAsync(int id, GenreDto updateGenreDto);
        Task<ServiceResponse<bool>> DeleteGenreAsync(int id);
    }
    

}
