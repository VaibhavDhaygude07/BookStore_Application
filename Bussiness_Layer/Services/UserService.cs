using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Services;
using DataAccess_Layer;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly BookStoreContext _context;
        private readonly EmailService _emailService;

        public UserService(IUserRepository repository, BookStoreContext context, EmailService emailService)
        {
            _repository = repository;
            _context = context;
            _emailService = emailService;
        }

        public async Task RegisterAsync(UserRegisterDto userDto)
        {
            await _repository.RegisterAsync(userDto);
        }

        public async Task<UserModel> LoginAsync(UserLoginDto loginDto)
        {
            return await _repository.LoginAsync(loginDto);
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
