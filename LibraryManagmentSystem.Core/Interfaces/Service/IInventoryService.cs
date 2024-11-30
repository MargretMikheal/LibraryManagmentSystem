using LibraryManagmentSystem.Domain.DTOs.InventoryDtos;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IInventoryService
    {
        Task<ServiceResponse<IEnumerable<InventoryDto>>> GetAllInventoriesAsync();
        Task<ServiceResponse<InventoryDetailsDto>> GetInventoryByBookIdAsync(int bookId);
        Task<ServiceResponse<string>> UpdateInventoryAsync(int bookId, UpdateInventoryDto updateInventoryDto);
    }
}
