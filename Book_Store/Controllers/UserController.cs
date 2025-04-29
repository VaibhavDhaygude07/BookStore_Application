using BookStore.Services.Services;
using Bussiness_Layer;
using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Services;
using DataAccess_Layer.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly JwtHelper _jwtHelper;
        private readonly EmailService _emailService;

        public UserController(IUserService service, JwtHelper jwtHelper, EmailService emailService)
        {
            _service = service;
            _jwtHelper = jwtHelper;
            _emailService = emailService; 
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
              
                await _service.RegisterAsync(dto);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _service.LoginAsync(dto);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtHelper.GenerateToken(user.EmailId, user.Role);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _service.UpdateUserAsync(user);

            return Ok(new
            {
                Message = "Login Successful",
                Token = token,
                RefreshToken = refreshToken,
                Role = user.Role
            });
        }

      
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _service.GetUserByEmailAsync(dto.EmailId);
            if (user == null)
                return NotFound(new { Success = false, Message = "User not found" });

            
            var token = _jwtHelper.GeneratePasswordResetToken(user.EmailId);

           
            var resetLink = $"{Request.Scheme}://{Request.Host}/api/user/reset-password?token={token}";

        
            await _emailService.SendEmailAsync(user.EmailId, "Reset Password", $"Click here to reset your password: <a href=\"{resetLink}\">{resetLink}</a>");


            return Ok(new { Success = true, Message = "Reset password link has been sent to your email" });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!string.Equals(dto.NewPassword?.Trim(), dto.ConfirmPassword?.Trim(), StringComparison.Ordinal))
                return BadRequest(new { Success = false, Message = "Passwords do not match" });

            var emailClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? dto.EmailId;
            if (string.IsNullOrEmpty(emailClaim))
                return BadRequest(new { Success = false, Message = "Invalid or missing email" });

            var isReset = await _service.ResetPasswordAsync(emailClaim, dto.NewPassword.Trim());
            if (!isReset)
                return NotFound(new { Success = false, Message = "User not found" });

            return Ok(new { Success = true, Message = "Password reset successful" });
        }



    }
}

