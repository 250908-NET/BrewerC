using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using School.Models;

namespace School.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IList<string> roles);
        string GenerateRefreshToken();
        Task<RefreshToken> CreateRefreshTokenAsync(string userId, string token, string ipAddress);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token, string ipAddress, string? replacedByToken = null);
        Task<bool> ValidateRefreshTokenAsync(string token);
        Task CleanupExpiredTokensAsync(string userId);
    }
}