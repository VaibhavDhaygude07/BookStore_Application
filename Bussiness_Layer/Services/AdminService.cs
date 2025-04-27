using Bussiness_Layer.Interfaces;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
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

        public Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto, string newPassword)
        {
         
            resetPasswordDto.NewPassword = newPassword;
            return _repository.ResetPasswordAsync(resetPasswordDto);
        }

        public Task<ResetPasswordDto> GetUserByEmailAsync(string? email)
        {
          
            return Task.FromResult(new ResetPasswordDto { EmailId = email });
        }
    }
}
