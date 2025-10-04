using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using School.Models;
using School.Repositories;

namespace School.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<TokenService> _logger;

        public TokenService(
            IConfiguration configuration, 
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        public string GenerateAccessToken(User user, IList<string> roles)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey missing");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim(role, "true")); // For your existing policies
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // Short-lived access token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(string userId, string token, string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7), // 7 days refresh token lifetime
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            var createdToken = await _refreshTokenRepository.CreateAsync(refreshToken);
            _logger.LogInformation("Refresh token created for user {UserId}", userId);

            return createdToken;
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _refreshTokenRepository.GetByTokenAsync(token);
        }

        public async Task RevokeRefreshTokenAsync(string token, string ipAddress, string? replacedByToken = null)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                _logger.LogWarning("Attempted to revoke invalid or inactive refresh token");
                return;
            }

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = replacedByToken;

            await _refreshTokenRepository.UpdateAsync(refreshToken);
            _logger.LogInformation("Refresh token revoked for user {UserId}", refreshToken.UserId);
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
            return refreshToken?.IsActive ?? false;
        }

        public async Task CleanupExpiredTokensAsync(string userId)
        {
            var expiredTokens = await _refreshTokenRepository.GetExpiredOrRevokedByUserIdAsync(userId);

            if (expiredTokens.Any())
            {
                await _refreshTokenRepository.DeleteRangeAsync(expiredTokens);
                
                _logger.LogInformation("Cleaned up {Count} expired tokens for user {UserId}", 
                    expiredTokens.Count(), userId);
            }
        }
    }
}