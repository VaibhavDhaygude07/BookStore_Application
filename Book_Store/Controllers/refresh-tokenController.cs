using Bussiness_Layer;
using Bussiness_Layer.Interfaces;
using DataAccess_Layer.DTO_s;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class refresh_tokenController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;
    private readonly JwtHelper _jwtHelper;

    public refresh_tokenController(IAdminService adminService, IUserService userService, JwtHelper jwtHelper)
    {
        _adminService = adminService;
        _userService = userService;
        _jwtHelper = jwtHelper;
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto tokenDto)
    {
        var principal = _jwtHelper.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var email = principal.Identity?.Name ?? principal.FindFirst(ClaimTypes.Email)?.Value;
        var role = principal.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
            return BadRequest(new { Success = false, Message = "Invalid token claims" });

        dynamic user;

        if (role == "Admin")
        {
            user = await _adminService.GetAdminByEmailAsync(email);
        }
        else
        {
            user = await _userService.GetUserByEmailAsync(email);
        }

        if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest(new { Success = false, Message = "Invalid refresh token" });
        }

        var newAccessToken = _jwtHelper.GenerateToken(user.EmailId, role);
        var newRefreshToken = _jwtHelper.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        if (role == "Admin")
            await _adminService.UpdateAdminAsync(user);
        else
            await _userService.UpdateUserAsync(user);

        return Ok(new
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
}
