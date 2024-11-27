using LibraryManagmentSystem.Domain.DTOs.BorrowingDTos;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IBorrowingService
    {
        Task<ServiceResponse<List<BorrowingDto>>> GetAllBorrowingsAsync();
        Task<ServiceResponse<BorrowingDto>> GetBorrowingByIdAsync(int id);
        Task<ServiceResponse<List<BorrowingDto>>> GetBorrowingsByUserAsync(string userId);
        Task<ServiceResponse<BorrowingDto>> CreateBorrowingAsync(CreateBorrowingDto createBorrowingDto);
        Task<ServiceResponse<string>> ReturnBookAsync(int id);
    }
}
