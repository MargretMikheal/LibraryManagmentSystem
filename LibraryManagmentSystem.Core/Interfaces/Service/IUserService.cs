using LibraryManagmentSystem.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<bool> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(string userId);
    }

}
