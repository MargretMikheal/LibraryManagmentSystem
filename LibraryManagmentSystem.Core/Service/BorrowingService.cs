using AutoMapper;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Core;
using LibraryManagmentSystem.Domain.DTOs.BorrowingDTos;

namespace LibraryManagementSystem.Core.Service
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<BorrowingDto>>> GetAllBorrowingsAsync()
        {
            var borrowings = await _unitOfWork.Borrowings.GetAllWithIncludesAsync(b => b.Book, b => b.User);
            var borrowingDtos = _mapper.Map<List<BorrowingDto>>(borrowings);

            return new ServiceResponse<List<BorrowingDto>> { Success = true, Data = borrowingDtos };
        }

        public async Task<ServiceResponse<BorrowingDto>> GetBorrowingByIdAsync(int id)
        {
            var borrowing = await _unitOfWork.Borrowings.GetByIdWithIncludesAsync(id, b => b.Book, b => b.User);
            if (borrowing == null)
                return new ServiceResponse<BorrowingDto> { Success = false, Message = "Borrowing not found." };

            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            return new ServiceResponse<BorrowingDto> { Success = true, Data = borrowingDto };
        }

        public async Task<ServiceResponse<List<BorrowingDto>>> GetBorrowingsByUserAsync(string userId)
        {
            var borrowings = await _unitOfWork.Borrowings.GetAllAsync(b => b.UserId == userId, includes: b => b.Book);
            var borrowingDtos = _mapper.Map<List<BorrowingDto>>(borrowings);

            return new ServiceResponse<List<BorrowingDto>> { Success = true, Data = borrowingDtos };
        }

        public async Task<ServiceResponse<BorrowingDto>> CreateBorrowingAsync(CreateBorrowingDto createBorrowingDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(createBorrowingDto.BookId);
            if (book == null)
                return new ServiceResponse<BorrowingDto> { Success = false, Message = "Book not found." };

            var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i => i.BookId == createBorrowingDto.BookId);
            if (inventory == null || inventory.AvailableCopies <= 0)
                return new ServiceResponse<BorrowingDto> { Success = false, Message = "Book is not available." };

            var borrowing = _mapper.Map<Borrowing>(createBorrowingDto);
            borrowing.BorrowedDate = DateTime.UtcNow;
            borrowing.DueDate = DateTime.UtcNow.AddDays(14); 


            inventory.AvailableCopies -= 1;
            await _unitOfWork.Borrowings.AddAsync(borrowing);
            _unitOfWork.Inventory.Update(inventory);
            await _unitOfWork.SaveChangesAsync();

            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
            borrowingDto.BookTitle = book.Title;
            return new ServiceResponse<BorrowingDto> { Success = true, Data = borrowingDto };
        }

        public async Task<ServiceResponse<string>> ReturnBookAsync(int id)
        {
            var borrowing = await _unitOfWork.Borrowings.GetByIdWithIncludesAsync(id, b => b.Book, b => b.User);
            if (borrowing == null)
                return new ServiceResponse<string> { Success = false, Message = "Borrowing not found." };

            var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(i => i.BookId == borrowing.BookId);
            if (inventory == null)
                return new ServiceResponse<string> { Success = false, Message = "Inventory record not found for the book." };

            borrowing.ReturnedDate = DateTime.UtcNow; 

            var overdueDays = (borrowing.ReturnedDate - borrowing.DueDate).Days;
            if (overdueDays > 0)
            {
                borrowing.FineAmount = overdueDays * 5;

                var fine = new Fine
                {
                    Amount = borrowing.FineAmount,
                    IsPaid = false,
                    BorrowingId = borrowing.Id,
                    UserId = borrowing.UserId
                };

                await _unitOfWork.Fines.AddAsync(fine);
            }

            inventory.AvailableCopies += 1;

            _unitOfWork.Borrowings.Update(borrowing);
            _unitOfWork.Inventory.Update(inventory);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<string>
            {
                Success = true,
                Message = overdueDays > 0 ? $"Book returned. Fine incurred: ${borrowing.FineAmount}" : "Book returned successfully."
            };
        }


    }
}
