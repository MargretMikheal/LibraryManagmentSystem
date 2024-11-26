using AutoMapper;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Domain.DTOs.BookDTOs;


namespace LibraryManagmentSystem.Core.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<BookDto>>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllWithIncludesAsync(b => b.Genre);
            var inventories = await _unitOfWork.Inventory.GetAllAsync();

            var bookDtos = books.Select(book =>
            {
                var availableCopies = inventories.FirstOrDefault(inv => inv.BookId == book.Id)?.AvailableCopies ?? 0;

                var bookDto = _mapper.Map<BookDto>(book);
                bookDto.CopiesAvailable = availableCopies;

                return bookDto;
            }).ToList();

            return new ServiceResponse<List<BookDto>>
            {
                Success = true,
                Data = bookDtos,
                Message = bookDtos.Count > 0 ? "Books retrieved successfully." : "No books found."
            };
        }


        public async Task<ServiceResponse<BookDto>> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdWithIncludesAsync(id, b => b.Genre);
            if (book == null)
            {
                return new ServiceResponse<BookDto>
                {
                    Success = false,
                    Message = "Book not found."
                };
            }

            var inventory = await _unitOfWork.Inventory.FirstOrDefaultAsync(inv => inv.BookId == id);
            var availableCopies = inventory?.AvailableCopies ?? 0;

            var bookDto = _mapper.Map<BookDto>(book);
            bookDto.CopiesAvailable = availableCopies;

            return new ServiceResponse<BookDto>
            {
                Success = true,
                Data = bookDto,
                Message = "Book retrieved successfully."
            };
        }


        public async Task<ServiceResponse<List<BookDto>>> GetBooksByGenreAsync(int genreId)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
            if (genre == null)
            {
                return new ServiceResponse<List<BookDto>>
                {
                    Success = false,
                    Message = "The specified genre does not exist."
                };
            }

            var books = await _unitOfWork.Books.GetAllAsync(b => b.GenreId == genreId, includes: b => b.Genre);
            var inventories = await _unitOfWork.Inventory.GetAllAsync();

            var bookDtos = books.Select(book =>
            {
                var availableCopies = inventories.FirstOrDefault(inv => inv.BookId == book.Id)?.AvailableCopies ?? 0;

                var bookDto = _mapper.Map<BookDto>(book);
                bookDto.CopiesAvailable = availableCopies;

                return bookDto;
            }).ToList();

            return new ServiceResponse<List<BookDto>>
            {
                Success = true,
                Data = bookDtos,
                Message = bookDtos.Count > 0
                    ? "Books retrieved successfully."
                    : "No books found for the specified genre."
            };
        }


        public async Task<ServiceResponse<BookDto>> AddBookAsync(AddBookDto addBookDto)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(addBookDto.GenreId);
            if (genre == null)
            {
                return new ServiceResponse<BookDto>
                {
                    Success = false,
                    Message = "The specified genre does not exist."
                };
            }

            var existingBook = await _unitOfWork.Books.FirstOrDefaultAsync(
                b => b.Title == addBookDto.Title && b.Author == addBookDto.Author);
            if (existingBook != null)
            {
                return new ServiceResponse<BookDto>
                {
                    Success = false,
                    Message = "The book already exists."
                };
            }

            var book = _mapper.Map<Book>(addBookDto);

            // Add book and save changes to generate its ID
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            // Create inventory for the new book
            var inventory = new Inventory
            {
                BookId = book.Id,
                TotalCopies = addBookDto.TotalCopies,
                AvailableCopies = addBookDto.TotalCopies
            };
            await _unitOfWork.Inventory.AddAsync(inventory);
            await _unitOfWork.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(book);

            return new ServiceResponse<BookDto>
            {
                Success = true,
                Message = "Book and inventory added successfully.",
                Data = bookDto
            };
        }


        public async Task<ServiceResponse<BookDto>> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return new ServiceResponse<BookDto>
                {
                    Success = false,
                    Message = "Book not found."
                };
            }

            _mapper.Map(updateBookDto, book);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();

            var updatedBookDto = _mapper.Map<BookDto>(book);

            return new ServiceResponse<BookDto>
            {
                Success = true,
                Data = updatedBookDto,
                Message = "Book updated successfully."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Book not found."
                };
            }

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Book deleted successfully."
            };
        }
    }
}
