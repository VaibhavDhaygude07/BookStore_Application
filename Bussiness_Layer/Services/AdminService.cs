using Bussiness_Layer.Interfaces;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using DataAccess_Layer.Repository;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository repository, IAdminRepository adminRepository)
        {
            _repository = repository;
            _adminRepository = adminRepository;
        }

        public Task RegisterAsync(AdminRegisterDto adminDto)
        {
            return _repository.RegisterAsync(adminDto);
        }

        public Task<AdminModel> LoginAsync(AdminLoginDto loginDto)
        {
            return _repository.LoginAsync(loginDto);
        }

        public Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            return _repository.ForgotPasswordAsync(forgotPasswordDto);
        }

        public Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            return _repository.ResetPasswordAsync(email, newPassword);
        }

        public Task<AdminModel> GetUserByEmailAsync(string email)
        {
            return _repository.GetUserByEmailAsync(email);
        }

        public async Task<AdminModel> GetAdminByEmailAsync(string email)
        {
            return await _adminRepository.GetAdminByEmailAsync(email);
        }

        public async Task UpdateAdminAsync(AdminModel admin)
        {
            await _adminRepository.UpdateAdminAsync(admin);
        }

    }
}
