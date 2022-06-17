using MaterialRequisition.Application.Constants;
using MaterialRequisition.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MaterialRequisition.Business.Implementations
{
    public class JWTManager: IJWTManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<JWTManager> _logger;

        public JWTManager(IHttpContextAccessor httpContextAccessor, ILogger<JWTManager> logger)
        {
            _contextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string GenerateToken(string username, int expirationInMinutes = 30)
        {
            var tokenResult = string.Empty;
            try
            {
                byte[] key = Convert.FromBase64String(JWTSecret.Key);
                SymmetricSecurityKey securityKey = new(key);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                SecurityTokenDescriptor descriptor = new()
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                JwtSecurityTokenHandler handler = new();
                JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
                tokenResult = handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown for: JWTManager.GenerateToken()");
            }

            return tokenResult;
        }

        public string GetCurrentlyLoggedInUsername()
        {
            var username = string.Empty;
            try
            {
                username = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown for: JWTManager.GetCurrentlyLoggedInUsername()");
            }

            return username;
        }
    }
}
