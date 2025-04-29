using DataAccess_Layer.DTO_s;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Threading.Tasks;

namespace DataAccess_Layer.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BookStoreContext _context;

        public AdminRepository(BookStoreContext context)
        {
            _context = context;
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
                Password = BCrypt.Net.BCrypt.HashPassword(adminDto.Password)
            };

            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<AdminModel> LoginAsync(AdminLoginDto loginDto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.EmailId == loginDto.EmailId);
            if (admin == null) return null;

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.Password);
            return isPasswordCorrect ? admin : null;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.EmailId == forgotPasswordDto.EmailId);
            return admin != null;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.EmailId == email);
            if (admin == null) return false;

            admin.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AdminModel> GetUserByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.EmailId == email);
        }

        public async Task<AdminModel> GetAdminByEmailAsync(string email)
        {
           
            return await _context.Admins.FirstOrDefaultAsync(a => a.EmailId == email);
        }

        public async Task UpdateAdminAsync(AdminModel admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

    }
}
