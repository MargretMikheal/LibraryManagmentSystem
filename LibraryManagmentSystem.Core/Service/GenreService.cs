using AutoMapper;
using LibraryManagementSystem.Domain.Models;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Domain.DTOs.GenreDtos;

namespace LibraryManagmentSystem.Core.Service
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetGenreDto>>> GetAllGenresAsync()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            var GetGenreDtos = _mapper.Map<List<GetGenreDto>>(genres);

            return new ServiceResponse<List<GetGenreDto>>
            {
                Success = true,
                Data = GetGenreDtos,
                Message = genres.Any() ? "Genres retrieved successfully." : "No genres available."
            };
        }

        public async Task<ServiceResponse<GetGenreDto>> GetGenreByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
                return new ServiceResponse<GetGenreDto> { Success = false, Message = "Genre not found." };

            var GetGenreDto = _mapper.Map<GetGenreDto>(genre);
            return new ServiceResponse<GetGenreDto> { Success = true, Data = GetGenreDto, Message = "Genre retrieved successfully." };
        }

        public async Task<ServiceResponse<GetGenreDto>> AddGenreAsync(GenreDto addGenreDto)
        {
            var existingGenre = await _unitOfWork.Genres.FirstOrDefaultAsync(g => g.Name == addGenreDto.Name);

            if (existingGenre != null)
            {
                return new ServiceResponse<GetGenreDto>
                {
                    Success = false,
                    Message = "A genre with the same name already exists."
                };
            }

            var genre = _mapper.Map<Genre>(addGenreDto);

            await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.SaveChangesAsync();

            var genreDto = _mapper.Map<GetGenreDto>(genre);

            return new ServiceResponse<GetGenreDto>
            {
                Success = true,
                Data = genreDto,
                Message = "Genre added successfully."
            };
        }


        public async Task<ServiceResponse<GetGenreDto>> UpdateGenreAsync(int id, GenreDto updateGetGenreDto)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
                return new ServiceResponse<GetGenreDto> { Success = false, Message = "Genre not found." };

            _mapper.Map(updateGetGenreDto, genre);
            _unitOfWork.Genres.Update(genre);
            await _unitOfWork.SaveChangesAsync();

            var GetGenreDto = _mapper.Map<GetGenreDto>(genre);
            return new ServiceResponse<GetGenreDto> { Success = true, Data = GetGenreDto, Message = "Genre updated successfully." };
        }

        public async Task<ServiceResponse<bool>> DeleteGenreAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
                return new ServiceResponse<bool> { Success = false, Data = false, Message = "Genre not found." };

            _unitOfWork.Genres.Delete(genre);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResponse<bool> { Success = true, Data = true, Message = "Genre deleted successfully." };
        }
    }
}
