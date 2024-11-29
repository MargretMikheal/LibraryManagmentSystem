using AutoMapper;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Service;
using LibraryManagmentSystem.Core;
using LibraryManagmentSystem.Domain.DTOs.FineDtos;
using Microsoft.AspNetCore.Identity;
using LibraryManagementSystem.Domain.Models;

public class FineService : IFineService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public FineService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ServiceResponse<IEnumerable<FineDto>>> GetAllFinesAsync()
    {
        var fines = await _unitOfWork.Fines.GetAllWithIncludesAsync(f => f.User, f => f.Borrowing);
        var fineDtos = _mapper.Map<IEnumerable<FineDto>>(fines);

        return new ServiceResponse<IEnumerable<FineDto>>
        {
            Success = true,
            Data = fineDtos
        };
    }

    public async Task<ServiceResponse<FineDetailsDto>> GetFineByIdAsync(int id)
    {
        var fine = await _unitOfWork.Fines.GetByIdWithIncludesAsync(id, f => f.User, f => f.Borrowing);
        if (fine == null)
        {
            return new ServiceResponse<FineDetailsDto>
            {
                Success = false,
                Message = "Fine not found."
            };
        }

        var fineDetailsDto = _mapper.Map<FineDetailsDto>(fine);
        return new ServiceResponse<FineDetailsDto>
        {
            Success = true,
            Data = fineDetailsDto
        };
    }

    public async Task<ServiceResponse<IEnumerable<FineDto>>> GetFinesByUserIdAsync(string userId)
    {
        var userExists = await _userManager.FindByIdAsync(userId);
        if (userExists == null)
        {
            return new ServiceResponse<IEnumerable<FineDto>>
            {
                Success = false,
                Message = "User not found."
            };
        }

        var fines = await _unitOfWork.Fines.GetAllAsync(f => f.UserId == userId, f => f.Borrowing);
        var fineDtos = _mapper.Map<IEnumerable<FineDto>>(fines);

        return new ServiceResponse<IEnumerable<FineDto>>
        {
            Success = true,
            Data = fineDtos
        };
    }


    public async Task<ServiceResponse<string>> MarkFineAsPaidAsync(int id)
    {
        var fine = await _unitOfWork.Fines.GetByIdAsync(id);
        if (fine == null)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Fine not found."
            };
        }
        fine.IsPaid = true;

        _unitOfWork.Fines.Update(fine);
        await _unitOfWork.Fines.SaveChangesAsync();

        return new ServiceResponse<string>
        {
            Success = true,
            Message = "Fine marked as paid."
        };
    }
}
