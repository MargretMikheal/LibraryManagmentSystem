using AutoMapper;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Domain.DTOs.InventoryDtos;

namespace LibraryManagmentSystem.Core.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<InventoryDto>>> GetAllInventoriesAsync()
        {
            var inventories = await _unitOfWork.Inventory.GetAllWithIncludesAsync(i => i.Book);
            var inventoryDtos = _mapper.Map<IEnumerable<InventoryDto>>(inventories);

            return new ServiceResponse<IEnumerable<InventoryDto>>
            {
                Success = true,
                Data = inventoryDtos
            };
        }

        public async Task<ServiceResponse<InventoryDetailsDto>> GetInventoryByBookIdAsync(int bookId)
        {
            var inventories = await _unitOfWork.Inventory.GetAllWithIncludesAsync(i => i.Book);
            var inventory = inventories.FirstOrDefault(i => i.BookId == bookId);

            if (inventory == null)
            {
                return new ServiceResponse<InventoryDetailsDto>
                {
                    Success = false,
                    Message = "Inventory record not found for the specified book."
                };
            }

            var inventoryDetailsDto = _mapper.Map<InventoryDetailsDto>(inventory);
            return new ServiceResponse<InventoryDetailsDto>
            {
                Success = true,
                Data = inventoryDetailsDto
            };
        }


        public async Task<ServiceResponse<string>> UpdateInventoryAsync(int bookId, UpdateInventoryDto updateInventoryDto)
        {
            var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i => i.BookId == bookId);
            if (inventory == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Inventory record not found for the specified book."
                };
            }

            inventory.TotalCopies = updateInventoryDto.TotalCopies;
            inventory.AvailableCopies = updateInventoryDto.AvailableCopies;

            _unitOfWork.Inventory.Update(inventory);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<string>
            {
                Success = true,
                Message = "Inventory updated successfully."
            };
        }
    }
}
