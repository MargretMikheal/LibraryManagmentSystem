using LibraryManagmentSystem.Domain.DTOs.FineDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IFineService
    {
        Task<ServiceResponse<IEnumerable<FineDto>>> GetAllFinesAsync();
        Task<ServiceResponse<FineDetailsDto>> GetFineByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<FineDto>>> GetFinesByUserIdAsync(string userId);
        Task<ServiceResponse<string>> MarkFineAsPaidAsync(int id);
    }

}
