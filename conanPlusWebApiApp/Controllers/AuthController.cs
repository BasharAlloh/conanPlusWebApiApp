using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using conanPlusWebApiApp.JWT;
using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommonRepository<User> _userRepository;
        private readonly ITokenManager _tokenManager;

        public AuthController(ICommonRepository<User> userRepository, ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        // دالة تسجيل الدخول (لم تتغير)
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDto)
        {
            if (userLoginDto == null || string.IsNullOrWhiteSpace(userLoginDto.Username) || string.IsNullOrWhiteSpace(userLoginDto.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var user = await _userRepository.GetUserByUsername(userLoginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid login attempt." });
            }

            var token = _tokenManager.GenerateToken(user.Username, user.Role, user.TokenVersion);

            return Ok(new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                Expiration = DateTime.Now.AddHours(1)
            });
        }

        [HttpPut("UpdateAdminCredentials")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAdminCredentials(UserUpdateDTO userUpdateDto)
        {
            if (userUpdateDto == null || (string.IsNullOrWhiteSpace(userUpdateDto.Username) && string.IsNullOrWhiteSpace(userUpdateDto.Password)))
            {
                return BadRequest(new { message = "Either a new Username or a new Password must be provided." });
            }

            var admin = await _userRepository.GetDetails(1);

            if (admin == null)
            {
                return NotFound(new { message = "Admin not found." });
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Username))
            {
                admin.Username = userUpdateDto.Username;
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Password))
            {
                admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
            }

            admin.TokenVersion += 1;
            await _userRepository.Update(admin);

            return Ok(new { message = "Admin credentials updated successfully." });
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "Invalid user context." });
            }

            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            user.TokenVersion += 1;
            await _userRepository.Update(user);

            return Ok(new { message = "Successfully logged out." });
        }

        [HttpPost("ValidateToken")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var token = _tokenManager.GetCurrentToken();

            if (string.IsNullOrEmpty(token) || !_tokenManager.ValidateToken(token))
            {
                return Unauthorized(new { message = "Token is invalid or expired." });
            }

            return Ok(new { message = "Token is valid." });
        }

    }
}
