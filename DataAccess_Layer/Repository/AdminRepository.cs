using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository
{
    public class AdminRepository: IAdminRepository
    {
        private readonly BookStoreContext _context;
        private readonly IPasswordHasher<AdminModel> _passwordHasher;

        public AdminRepository(BookStoreContext context, IPasswordHasher<AdminModel> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterAsync(AdminRegisterDto adminDto)
        {
            if (await _context.Admins.AnyAsync(x => x.EmailId == adminDto.EmailId))
                throw new Exception("Email already registered");

            var admin = new AdminModel
            {
                FullName = adminDto.FullName,
                EmailId = adminDto.EmailId,
                MobileNumber = adminDto.MobileNumber,
                Role = "Admin",
            };
            admin.Password = _passwordHasher.HashPassword(admin, adminDto.Password);

            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<AdminModel> LoginAsync(AdminLoginDto loginDto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.EmailId == loginDto.EmailId);
            if (admin == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(admin, admin.Password, loginDto.Password);
            return result == PasswordVerificationResult.Success ? admin : null;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.EmailId == forgotPasswordDto.EmailId);
            return admin != null;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.EmailId == resetPasswordDto.EmailId);
            if (admin == null)
                throw new Exception("Admin not found");

            admin.Password = _passwordHasher.HashPassword(admin, resetPasswordDto.NewPassword);
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
