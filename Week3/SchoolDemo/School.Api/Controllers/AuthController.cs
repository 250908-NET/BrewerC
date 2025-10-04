using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.DTO;
using School.Services;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register/student")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDto dto)
        {
            var student = new Student
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(student, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Ensure "Student" role exists
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }

            await _userManager.AddToRoleAsync(student, "Student");

            _logger.LogInformation("Student registered: {Email}", dto.Email);

            return Ok(new { message = "Student registered successfully" });
        }

        [HttpPost("register/instructor")]
        public async Task<IActionResult> RegisterInstructor([FromBody] RegisterInstructorDto dto)
        {
            var instructor = new Instructor
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(instructor, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Ensure "Instructor" role exists
            if (!await _roleManager.RoleExistsAsync("Instructor"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Instructor"));
            }

            await _userManager.AddToRoleAsync(instructor, "Instructor");

            _logger.LogInformation("Instructor registered: {Email}", dto.Email);

            return Ok(new { message = "Instructor registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenService.GenerateAccessToken(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var ipAddress = GetIpAddress();
            await _tokenService.CreateRefreshTokenAsync(user.Id, refreshToken, ipAddress);

            await _tokenService.CleanupExpiredTokensAsync(user.Id);

            SetRefreshTokenCookie(refreshToken);

            _logger.LogInformation("User logged in: {Email}", dto.Email);

            return Ok(new AuthResponseDto
            {
                Token = accessToken,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Refresh token is required" });
            }

            var storedToken = await _tokenService.GetRefreshTokenAsync(refreshToken);

            if (storedToken == null || !storedToken.IsActive)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token, login again" });
            }

            var user = storedToken.User;
            var roles = await _userManager.GetRolesAsync(user);

            // Generate new tokens
            var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Revoke old refresh token and create new one
            var ipAddress = GetIpAddress();
            await _tokenService.RevokeRefreshTokenAsync(refreshToken, ipAddress, newRefreshToken);
            await _tokenService.CreateRefreshTokenAsync(user.Id, newRefreshToken, ipAddress);

            // Set new refresh token cookie
            SetRefreshTokenCookie(newRefreshToken);

            _logger.LogInformation("Token refreshed for user: {Email}", user.Email);

            return Ok(new AuthResponseDto
            {
                Token = newAccessToken,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new { message = "Refresh token is required" });
            }

            var ipAddress = GetIpAddress();
            await _tokenService.RevokeRefreshTokenAsync(refreshToken, ipAddress);

            // Clear the cookie
            Response.Cookies.Delete("refreshToken");

            _logger.LogInformation("Token revoked");

            return Ok(new { message = "Token revoked successfully" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                var ipAddress = GetIpAddress();
                await _tokenService.RevokeRefreshTokenAsync(refreshToken, ipAddress);
            }

            // Clear the cookie
            Response.Cookies.Delete("refreshToken");

            _logger.LogInformation("User logged out");

            return Ok(new { message = "Logged out successfully" });
        }

        // Helper methods
        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production with HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"].ToString();
            }

            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "unknown";
        }
    }
}