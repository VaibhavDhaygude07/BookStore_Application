using BookStore.Services.Services;
using Bussiness_Layer;
using Bussiness_Layer.Interfaces;
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

        public UserController(IUserService service, JwtHelper jwtHelper)
        {
            _service = service;
            _jwtHelper = jwtHelper;
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

            return Ok(new
            {
                Message = "Login Successful",
                Token = token,
                Role = user.Role
            });
        }
    }
}

