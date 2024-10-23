using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.JWT
{
    public class TokenManager : ITokenManager
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private static List<string> invalidatedTokens = new List<string>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonRepository<User> _userRepository;

        public TokenManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ICommonRepository<User> userRepository)
        {
            _key = configuration["JWT:Key"];
            _issuer = configuration["JWT:Issuer"];
            _audience = configuration["JWT:Audience"];
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public string GenerateToken(string username, string role, int tokenVersion)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("TokenVersion", tokenVersion.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (IsTokenInvalidated(token))
                {
                    return false;
                }

                var tokenVersionClaim = claimsPrincipal.FindFirst("TokenVersion")?.Value;
                var currentTokenVersion = GetUserTokenVersionAsync(claimsPrincipal.Identity.Name).Result;

                if (string.IsNullOrEmpty(tokenVersionClaim) ||
                    !int.TryParse(tokenVersionClaim, out int userTokenVersion) ||
                    userTokenVersion != currentTokenVersion)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void InvalidateCurrentToken()
        {
            var token = GetCurrentToken();
            if (!string.IsNullOrEmpty(token))
            {
                invalidatedTokens.Add(token);
            }
        }

        public bool IsTokenInvalidated(string token)
        {
            return invalidatedTokens.Contains(token);
        }

        public async Task<int> GetUserTokenVersionAsync(string username)
        {
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Username == username);
            return user?.TokenVersion ?? 1;
        }

        public string GetCurrentToken()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            return authHeader?.StartsWith("Bearer ") == true ? authHeader.Substring("Bearer ".Length).Trim() : null;
        }
    }
}
