using Bussiness_Layer;
using Bussiness_Layer.Interfaces;
using DataAccess_Layer.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class refresh_tokenController : ControllerBase
    {
        public readonly IAdminService _service;
        private readonly JwtHelper _jwtHelper;


        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh(RefreshTokenDto tokenDto)
        {
            var principal = _jwtHelper.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var email = principal.Identity.Name ?? principal.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _service.GetAdminByEmailAsync(email);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest(new { Success = false, Message = "Invalid refresh token" });
            }

            var newAccessToken = _jwtHelper.GenerateToken(user.EmailId, user.Role);
            var newRefreshToken = _jwtHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _service.UpdateAdminAsync(user);

            return Ok(new
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

    }
}
