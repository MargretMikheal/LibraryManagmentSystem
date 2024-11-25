using LibraryManagmentSystem.Domain.DTOs.BookDTOs;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IBookService
    {
        Task<ServiceResponse<List<BookDto>>> GetAllBooksAsync();
        Task<ServiceResponse<BookDto>> GetBookByIdAsync(int id);
        Task<ServiceResponse<List<BookDto>>> GetBooksByGenreAsync(int genreId);
        Task<ServiceResponse<BookDto>> AddBookAsync(AddBookDto addBookDto);
        Task<ServiceResponse<BookDto>> UpdateBookAsync(int id, UpdateBookDto updateBookDto);
        Task<ServiceResponse<bool>> DeleteBookAsync(int id);
    }
}
