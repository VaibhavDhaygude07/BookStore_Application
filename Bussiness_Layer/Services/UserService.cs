using Bussiness_Layer.Interfaces;
using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;

namespace BookStore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterAsync(UserRegisterDto userDto)
        {
            await _repository.RegisterAsync(userDto);
        }

        public async Task<UserModel> LoginAsync(UserLoginDto loginDto)
        {
            return await _repository.LoginAsync(loginDto);
        }
    }
}
