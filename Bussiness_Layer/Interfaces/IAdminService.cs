using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Models;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
    public interface IAdminService
    {
        Task RegisterAsync(AdminRegisterDto adminDto);
        Task<AdminModel> LoginAsync(AdminLoginDto loginDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
        Task<AdminModel> GetUserByEmailAsync(string email);
        Task<AdminModel> GetAdminByEmailAsync(string email);
        Task UpdateAdminAsync(AdminModel admin);
    }
}
