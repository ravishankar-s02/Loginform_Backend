using Microsoft.AspNetCore.Mvc;
using AuthApi.Models;
using AuthApi.Services;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            bool isValid = await _authService.ValidateUserAsync(login.UserName, login.Password);
            return isValid
                ? Ok(new { message = "Login successful" })
                : Unauthorized(new { message = "Invalid credentials" });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserModel user)
        {
            if (await _authService.UserExistsAsync(user.UserName))
                return Conflict(new { message = "Username already exists" });

            bool success = await _authService.RegisterUserAsync(user);
            return success
                ? Ok(new { message = "User registered successfully" })
                : Problem("Signup failed");
        }
    }
}
