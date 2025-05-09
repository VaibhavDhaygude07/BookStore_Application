﻿using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Models;
using DataAccess_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreContext _context;
        private readonly IPasswordHasher<UserModel> _passwordHasher;

        public UserRepository(BookStoreContext context, IPasswordHasher<UserModel> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterAsync(UserRegisterDto userDto)
        {
            if (await _context.Users.AnyAsync(x => x.EmailId == userDto.EmailId))
                throw new Exception("Email already registered");

            var user = new UserModel
            {
                FullName = userDto.FullName,
                EmailId = userDto.EmailId,
                MobileNumber = userDto.MobileNumber,
                Role = "User",
            };
            user.Password = _passwordHasher.HashPassword(user, userDto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == loginDto.EmailId);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == forgotPasswordDto.EmailId);
            return user != null;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == email);
            if (user == null)
                return false;

            user.Password = _passwordHasher.HashPassword(user, newPassword);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
