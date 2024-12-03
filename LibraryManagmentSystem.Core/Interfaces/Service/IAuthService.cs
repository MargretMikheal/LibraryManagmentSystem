using LibraryManagmentSystem.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> Login(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<ServiceResponse<string>> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        Task<ServiceResponse<string>> ForgotPasswordAsync(string email);
        Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }

}
