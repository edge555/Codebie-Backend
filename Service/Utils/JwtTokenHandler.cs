using Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Utils
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JwtTokenHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.ToString())
            };
            var configKey = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: signingCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        public string? GetLoggedInUserId()
        {
            string id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return id;
        }
        public Boolean IsTokenExpired()
        {
            var tokenGenerationTimeString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Expiration).Value;
            if (tokenGenerationTimeString == null)
            {
                return true;
            }
            var tokenGenerationTime = Convert.ToDateTime(tokenGenerationTimeString);
            DateTime expirationTime = tokenGenerationTime.AddDays(3);
            DateTime currentTime = DateTime.UtcNow;
            return currentTime > expirationTime;
        }
        public void DeleteToken()
        {
            _httpContextAccessor.HttpContext = null;
        }
    }
}
