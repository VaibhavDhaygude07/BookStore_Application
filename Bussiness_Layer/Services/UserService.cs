using Bussiness_Layer.Interfaces;
using DataAccess_Layer;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using DataAccess_Layer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly BookStoreContext _context;

        public UserService(IUserRepository repository, BookStoreContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task RegisterAsync(UserRegisterDto userDto)
        {
            await _repository.RegisterAsync(userDto);
        }

        public async Task<UserModel> LoginAsync(UserLoginDto loginDto)
        {
            return await _repository.LoginAsync(loginDto);
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            return await _repository.ForgotPasswordAsync(forgotPasswordDto);
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            return await _repository.ResetPasswordAsync(email, newPassword);
        }


        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailId == email);
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            await _repository.UpdateUserAsync(user);
        }
    }
}
