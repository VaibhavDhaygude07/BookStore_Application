using Bussiness_Layer;
using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Services;
using DataAccess_Layer.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly JwtHelper _jwtHelper;
        private readonly EmailService _emailService;

        public adminController(IAdminService service, JwtHelper jwtHelper, EmailService emailService)
        {
            _service = service;
            _jwtHelper = jwtHelper;
            _emailService = emailService; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AdminRegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }


                await _service.RegisterAsync(dto);
                return Ok(new { Success = true, Message = "Admin registered successfully", Data = dto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginDto dto)
        {
            var admin = await _service.LoginAsync(dto);
            if (admin == null)
                return Unauthorized(new { Success = false, Message = "Invalid credentials" });

            var token = _jwtHelper.GenerateToken(admin.Id, admin.EmailId, admin.Role);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            admin.RefreshToken = refreshToken;
            admin.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _service.UpdateAdminAsync(admin);

            return Ok(new
            {
                Success = true,
                Message = "Login Successful",
                Token = token,
                RefreshToken = refreshToken,
                Role = admin.Role
            });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _service.GetUserByEmailAsync(dto.EmailId);
            if (user == null)
                return NotFound(new { Success = false, Message = "User not found" });

            var token = _jwtHelper.GeneratePasswordResetToken(user.EmailId);
            var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?token={token}";

            await _emailService.SendEmailAsync(user.EmailId, "Reset Password", $"Click here to reset your password: <a href=\"{resetLink}\">{resetLink}</a>");

            return Ok(new { Success = true, Message = "Reset password link has been sent to your email" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var principal = _jwtHelper.ValidatePasswordResetToken(dto.Token);
            if (principal == null)
                return BadRequest(new { Success = false, Message = "Invalid token" });

            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                return BadRequest(new { Success = false, Message = "Invalid token" });

            if (!string.Equals(dto.NewPassword?.Trim(), dto.ConfirmPassword?.Trim(), StringComparison.Ordinal))
                return BadRequest(new { Success = false, Message = "Passwords do not match" });

            var success = await _service.ResetPasswordAsync(email, dto.NewPassword.Trim());
            if (!success)
                return NotFound(new { Success = false, Message = "User not found" });

            return Ok(new { Success = true, Message = "Password reset successful" });
        }

    }
}
