using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Interfaces
{
    public interface IAdminRepository
    {
        Task RegisterAsync(AdminRegisterDto adminDto);
        Task<AdminModel> LoginAsync(AdminLoginDto loginDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
