using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterDto userDto);
        Task<UserModel> LoginAsync(UserLoginDto loginDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(UserModel user);

    }
}
